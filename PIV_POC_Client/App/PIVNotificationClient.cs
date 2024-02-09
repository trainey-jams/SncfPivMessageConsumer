using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.PivMessage.Root;
using System.Diagnostics;
using System.Timers;

namespace PIV_POC_Client.App
{
    public class PIVNotificationClient
    {
        private readonly IOpenWireSessionFactory SessionFactory;
        private readonly IActiveMQMapper Mapper;
        private readonly ISqsRepository SqsRepository;
        
        private static int processedMessages = 0;
        private static int totalMessages = 0;
        private static long totalSeconds = 0;
        private static int FailedMessages = 0;

        public PIVNotificationClient(
            IOpenWireSessionFactory sessionFactory, 
            IActiveMQMapper mapper,
            ISqsRepository sqsRepository)
        {
            SessionFactory = sessionFactory;
            Mapper = mapper;
            SqsRepository = sqsRepository;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"{processedMessages} messages processed in last 30 seconds.");
            processedMessages = 0;
        }

        public async Task GetMessages()
        {
            double interval = 30000.0;
            var timer = new System.Timers.Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.AutoReset = true;
            timer.Enabled = true;
           
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<ActiveMQMessage> messages = new List<ActiveMQMessage>();

            using var session = await SessionFactory.GetSession(AcknowledgementMode.IndividualAcknowledge);

            using var dest = await session.GetTopicAsync("VirtualTopic.circulationsEnrichies.v2");
            using MessageConsumer consumer = (MessageConsumer)await session.CreateConsumerAsync(dest);

            var cancellationToken = new CancellationTokenSource();
            var token = cancellationToken.Token;

            Console.WriteLine("Connected, session started.");

            var task = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            var rawMessage = await consumer.ReceiveAsync() as ActiveMQMessage;

                            messages.Add(rawMessage);
                        }

                        Parallel.ForEach(messages, async rawMessage =>
                        {
                            PivMessageRoot mappedMessage = Mapper.Map(rawMessage);

                            string mappedMessageStr = JsonConvert.SerializeObject(mappedMessage);

                            if (await SqsRepository.PublishMessage(mappedMessageStr))
                            {
                                await rawMessage.AcknowledgeAsync();
                            }
                            else
                            {
                                FailedMessages++;
                                Console.WriteLine($"There are {FailedMessages} failed messages.");
                            }
                        });

                        Console.WriteLine($"Processed message batch. There are now {consumer.UnconsumedMessageCount} unconsumed messages.");

                        processedMessages +=50;
                        totalMessages+=50;
                        messages.Clear();
                    }

                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                        cancellationToken.Cancel();
                    }
                }

               
            }, token);

            Console.ReadKey();
    
            totalSeconds = stopwatch.ElapsedMilliseconds / 1000;
            
            cancellationToken.Cancel();


            if (token.IsCancellationRequested)
            {
                Console.WriteLine("\n");
                Console.WriteLine("*********************************************************");
                Console.WriteLine("Task cancelled by user, disconnecting from message stream.");
                Console.WriteLine("*********************************************************");

                timer.Stop();
                Console.WriteLine($"{totalMessages} messages have been processed in {totalSeconds} seconds.");

                await Task.Delay(5000);
            }
        }
    }
}

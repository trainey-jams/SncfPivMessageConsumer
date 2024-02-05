using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.AWS.Repos;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.App
{
    public class PIVNotificationClient
    {
        private readonly IOpenWireSessionFactory SessionFactory;
        private readonly IActiveMQMapper Mapper;
        private readonly IS3Repository S3Repository;
        private readonly ISqsRepository SqsRepository;
        private readonly IDynamoDbRepository DynamoDbRepository;

        public PIVNotificationClient(
            IOpenWireSessionFactory sessionFactory, 
            IActiveMQMapper mapper,
            IS3Repository s3Repository,
            ISqsRepository sqsRepository,
            IDynamoDbRepository dynamoDbRepository)
        {
            SessionFactory = sessionFactory;
            Mapper = mapper;
            S3Repository = s3Repository; 
            SqsRepository = sqsRepository;
            DynamoDbRepository = dynamoDbRepository;
        }

        public async Task GetMessages()
        {
            using var session = await SessionFactory.GetSession();

            using var dest = await session.GetTopicAsync("VirtualTopic.circulationsEnrichies.v2");
            using var consumer = await session.CreateConsumerAsync(dest);

            var cancellationToken = new CancellationTokenSource();
            var token = cancellationToken.Token;

            Console.WriteLine("Connected, session started.");

            var task = Task.Run(async () =>
            {
                var msg = await consumer.ReceiveAsync() as ActiveMQMessage;

          

                try
                {
                    PivMessageRoot root = Mapper.Map(msg);

                    await DynamoDbRepository.SaveAsync(root);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
             
               // await SqsRepository.PublishMessage(JsonConvert.SerializeObject(root));
            }, token);

            Console.ReadKey();
            cancellationToken.Cancel();

            if (token.IsCancellationRequested)
            {
                Console.WriteLine("\n");
                Console.WriteLine("*********************************************************");
                Console.WriteLine("Task cancelled by user, disconnecting from message stream.");
                Console.WriteLine("*********************************************************");

                await Task.Delay(5000);
            }

            // ConcurrentQueue<SendMessageBatchRequestEntry> MessagesToSQS = new ConcurrentQueue<SendMessageBatchRequestEntry>();
            //Thread worker1 =
            //                new Thread(async async =>
            //                {
            //                    SendMessageBatchRequestEntry e = await MessageService.ProcessMessage(client, guid1);

            //                    if (!string.IsNullOrWhiteSpace(e.MessageBody))
            //                    {
            //                        MessagesToSQS.Enqueue(e);
            //                    }

            //                });

            //SendMessageBatchRequestEntry entry;
            //if (MessagesToSQS.Count > 10)
            //{
            //    List<SendMessageBatchRequestEntry> msgs = new List<SendMessageBatchRequestEntry>();

            //    for (int i = 0; i < 3; i++)
            //    {
            //        if (MessagesToSQS.TryDequeue(out entry))
            //        {
            //            msgs.Add(entry);
            //        }

            //    }
            //    try
            //    {
            //        await S3Repository.SendMessageBatchToS3(msgs);
            //    }

            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.ToString());
            //    }
            //}
        }
    }
}

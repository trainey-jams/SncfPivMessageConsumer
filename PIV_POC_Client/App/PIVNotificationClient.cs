using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using PIV_POC_Client.DAL.Repositories;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.PivMessage.Root;
using PIV_POC_Client.Processor;
using System.Collections.Concurrent;

namespace PIV_POC_Client.App
{
    public class PIVNotificationClient
    {
        private readonly IWebSocketClientFactory ClientFactory;
        private readonly IStompClientFrameWrapper ClientFrameWrapper;
        private readonly IMessageService MessageService;
        private readonly NotificationClientConfiguration NotificationClientConfiguration;
        private readonly ISqsRepository SqsRepository;
        
        public PIVNotificationClient(
            IWebSocketClientFactory clientFactory, 
            IStompClientFrameWrapper clientFrameWrapper,
            IMessageService messageService,
            IOptions<NotificationClientConfiguration> notificationClientConfiguration,
            ISqsRepository sqsRepository)
        {
            ClientFactory = clientFactory;
            ClientFrameWrapper = clientFrameWrapper;
            MessageService = messageService;
            NotificationClientConfiguration = notificationClientConfiguration.Value;
            SqsRepository = sqsRepository;  
        }

        public async Task GetMessages()
        {
            using (var client = await ClientFactory.GetClient(NotificationClientConfiguration.WebsocketClientConfiguration))
            {
                Console.WriteLine($"Client status {client.State}");
               
                var cancellationToken = new CancellationTokenSource();
                var token = cancellationToken.Token;

                await ClientFrameWrapper.Connect(client, NotificationClientConfiguration.ConnectConfiguration);
                await ClientFrameWrapper.Subscribe(client, NotificationClientConfiguration.SubscribeConfiguration);


                Guid guid1 = Guid.Parse("529b8734-2b78-4a1e-892c-46c4c899342a");
                Guid guid2 = Guid.Parse("38daf51f-169d-4a79-90af-c16950724a78");
                Guid guid3 = Guid.Parse("d3dfe650-67a9-4a86-a9d7-06ed1b013434");

                ConcurrentQueue<SendMessageBatchRequestEntry> MessagesToSQS = new ConcurrentQueue<SendMessageBatchRequestEntry>();

                var task = Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        //Thread worker1 = new Thread(async async => { await MessageService.ProcessMessage(client, guid1); });
                        //Thread worker2 = new Thread(async async => { await MessageService.ProcessMessage(client, guid2); });
                        //Thread worker3 = new Thread(async async => { await MessageService.ProcessMessage(client, guid3); });

                        Thread worker1 = 
                            new Thread(async async => 
                            {
                                SendMessageBatchRequestEntry e = await MessageService.ProcessMessage(client, guid1);

                                if(!string.IsNullOrWhiteSpace(e.MessageBody))
                                {
                                    MessagesToSQS.Enqueue(e);
                                }
                            
                            });
                        //Thread worker2 = new Thread(async async => { MessagesToSQS.Enqueue(await MessageService.ProcessMessage(client, guid2)); });
                        //Thread worker3 = new Thread(async async => { MessagesToSQS.Enqueue(await MessageService.ProcessMessage(client, guid3)); });

                        worker1.Start();
                        //worker2.Start();
                        //worker3.Start();

                        worker1.Join();
                        //worker2.Join();
                        //worker3.Join();





                        SendMessageBatchRequestEntry entry;
                        if (MessagesToSQS.Count > 10)
                        {
                            List<SendMessageBatchRequestEntry> msgs = new List<SendMessageBatchRequestEntry>();

                            for (int i = 0; i < 3; i++)
                            {
                                if (MessagesToSQS.TryDequeue(out entry))
                                {
                                    msgs.Add(entry);
                                }

                            }
                            try
                            {
                                await SqsRepository.PublishMessageBatch(msgs);
                            }

                            catch (Exception ex) 
                            {
                            
                                Console.WriteLine(ex.ToString());
                            }


                           
                        }

                          Console.WriteLine($"Number of current threads is {System.Diagnostics.Process.GetCurrentProcess().Threads.Count}");
                    }
                }, token);

                ///pseudo code
                ///

                Console.ReadKey();
                cancellationToken.Cancel();

                if (token.IsCancellationRequested)
                {
                    await ClientFrameWrapper.Disconnect(client, NotificationClientConfiguration.ReceiptId);

                    Console.WriteLine("\n");
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine("Task cancelled by user, disconnecting from message stream.");
                    Console.WriteLine("*********************************************************");

                    await Task.Delay(5000);
                }

                task.Wait(token);
            }

            Console.WriteLine("\n");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("WebSocket closed, shutting down.");
            Console.WriteLine("*********************************************************");
            await Task.Delay(4000);
        }
    }
}

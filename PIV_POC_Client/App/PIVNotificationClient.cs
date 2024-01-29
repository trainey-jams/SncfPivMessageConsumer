using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;

namespace PIV_POC_Client.App
{
    public class PIVNotificationClient
    {
        private readonly IWebSocketClientFactory ClientFactory;
        private readonly IStompClientFrameWrapper ClientFrameWrapper;
        private readonly IMessageService MessageService;
        private readonly NotificationClientConfiguration NotificationClientConfiguration;
        
        public PIVNotificationClient(
            IWebSocketClientFactory clientFactory, 
            IStompClientFrameWrapper clientFrameWrapper,
            IMessageService messageService,
            IOptions<NotificationClientConfiguration> notificationClientConfiguration)
        {
            ClientFactory = clientFactory;
            ClientFrameWrapper = clientFrameWrapper;
            MessageService = messageService;
            NotificationClientConfiguration = notificationClientConfiguration.Value;
        }

        public async Task GetMessages()
        {
            using (var client = await ClientFactory.GetClient(NotificationClientConfiguration.WebsocketClientConfiguration))
            {
                Console.WriteLine($"Client status {client.State}");
               
                var cancellationToken = new CancellationTokenSource();
                var token = cancellationToken.Token;

                await ClientFrameWrapper.Connect(client, NotificationClientConfiguration.ConnectConfiguration);

                var task = Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                       await MessageService.ProcessMessage(client, NotificationClientConfiguration.ConnectConfiguration.ClientId);
                    }
                }, token);

                await ClientFrameWrapper.Subscribe(client, NotificationClientConfiguration.SubscribeConfiguration);

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

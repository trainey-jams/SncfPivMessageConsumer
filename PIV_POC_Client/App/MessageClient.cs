using Apache.NMS.ActiveMQ;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config.Openwire;

namespace PIV_POC_Client.App
{
    public class MessageClient
    {
        private readonly ILogger<MessageClient> Logger;
        private readonly SessionConfig SessionConfig;
        private readonly IOpenWireSessionFactory SessionFactory;
        private readonly IMessageService MessageService;

        public MessageClient(
            ILogger<MessageClient> logger,  
            IOptions<SessionConfig> sessionConfig,
            IOpenWireSessionFactory sessionFactory, 
            IMessageService messageService)
        {
            Logger = logger;
            SessionConfig = sessionConfig.Value;
            SessionFactory = sessionFactory;
            MessageService = messageService;
        }

        public async Task EstablishPivBrokerConnection()
        {
            var cancellationToken = new CancellationTokenSource();
            var token = cancellationToken.Token;

            await MessageService.ProcessPIVMessages(token);

            Console.ReadKey();
            
            cancellationToken.Cancel();

            if (token.IsCancellationRequested)
            {
                Logger.LogInformation("Cancellation has been requested, disconnecting from broker and shutting down.");

                await Task.Delay(5000);
            }
        }
    }
}

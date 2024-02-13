using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
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
            using var session = await SessionFactory.GetSession(SessionConfig.AcknowledgementMode);

            using var dest = await session.GetTopicAsync(SessionConfig.TopicName);
            using MessageConsumer consumer = (MessageConsumer)await session.CreateConsumerAsync(dest);

            var cancellationToken = new CancellationTokenSource();
            var token = cancellationToken.Token;

            Logger.LogInformation("Connected, session started.");

            await MessageService.ProcessPIVMessages(consumer, token);

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
        }
    }
}

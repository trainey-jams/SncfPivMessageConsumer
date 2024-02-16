using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.PivMessage.Root;
using PIV_POC_Client.Utility;
using System.Timers;

namespace PIV_POC_Client.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly MessageServiceConfig ServiceConfig;
        private readonly IActiveMQMapper Mapper;
        private readonly ISnsRepository SnsRepository;

        public MessageService(ILogger<MessageService> logger, IOptions<MessageServiceConfig> serviceConfig, IActiveMQMapper mapper, ISnsRepository snsRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceConfig = serviceConfig.Value ?? throw new ArgumentNullException(nameof(serviceConfig));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SnsRepository = snsRepository ?? throw new ArgumentNullException(nameof(snsRepository));
        }

        public async Task ProcessPIVMessages(IMessageConsumer consumer, CancellationToken cancellationToken)
        {
            List<ActiveMQMessage> messages = new List<ActiveMQMessage>();

            var task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        for (int i = 0; i < ServiceConfig.BatchSize; i++)
                        {
                            var rawMessage = await consumer.ReceiveAsync() as ActiveMQMessage;

                            messages.Add(rawMessage);
                        }

                        Parallel.ForEach(messages, async rawMessage =>
                        {
                            PivMessageRoot mappedMessage = Mapper.Map(rawMessage);

                            string messageStr = TranslationSerializer.Serialize(mappedMessage, true);

                            if (await SnsRepository.PublishMessage(messageStr))
                            {
                                await rawMessage.AcknowledgeAsync();
                            }
                            else
                            {
                                Logger.LogWarning($"Failed to process message with key: {mappedMessage.MessageId}");
                            }
                        });

                        Logger.LogInformation($"There are now {((MessageConsumer)consumer).UnconsumedMessageCount} unconsumed messages.");

                        messages.Clear();
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }
                }
            }, cancellationToken);
        }
    }
}

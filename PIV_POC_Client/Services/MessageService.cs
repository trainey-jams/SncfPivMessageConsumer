﻿using Apache.NMS;
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
        private readonly ISqsRepository SqsRepository;

        public MessageService(ILogger<MessageService> logger, IOptions<MessageServiceConfig> serviceConfig, IActiveMQMapper mapper, ISqsRepository sqsRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceConfig = serviceConfig.Value ?? throw new ArgumentNullException(nameof(serviceConfig));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SqsRepository = sqsRepository ?? throw new ArgumentNullException(nameof(sqsRepository));
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

                            //string mappedMessageStr = JsonConvert.SerializeObject(mappedMessage);

                            string mappedMessageStr = TranslationSerializer.Serialize(mappedMessage, true);

                            if (await SqsRepository.PublishMessage(mappedMessageStr))
                            {
                                await rawMessage.AcknowledgeAsync();
                            }
                            else
                            {
                                Logger.LogWarning($"Failed to process message with id: {rawMessage.MessageId}");
                            }
                        });

                        Logger.LogInformation($"Processed message batch of {ServiceConfig.BatchSize} messages. There are now {((MessageConsumer)consumer).UnconsumedMessageCount} unconsumed messages.");

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

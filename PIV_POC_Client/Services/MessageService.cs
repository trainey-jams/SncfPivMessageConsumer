using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.PivMessage.Root;
using System.Timers;

namespace PIV_POC_Client.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly MessageServiceConfig ServiceConfig;
        private readonly IActiveMQMapper Mapper;
        private readonly ISqsRepository SqsRepository;

        private static int processedMessages = 0;
        private static int totalMessages = 0;
        private static int FailedMessages = 0;

        public MessageService(ILogger<MessageService> logger, IOptions<MessageServiceConfig> serviceConfig, IActiveMQMapper mapper, ISqsRepository sqsRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceConfig = serviceConfig.Value ?? throw new ArgumentNullException(nameof(serviceConfig));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SqsRepository = sqsRepository ?? throw new ArgumentNullException(nameof(sqsRepository));
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Logger.LogInformation($"{processedMessages} messages processed in last 30 seconds.");
            processedMessages = 0;
        }

        public async Task ProcessPIVMessages(IMessageConsumer consumer, CancellationToken cancellationToken)
        {
            double interval = 30000.0;
            var timer = new System.Timers.Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.AutoReset = true;
            timer.Enabled = true;

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

                            string mappedMessageStr = JsonConvert.SerializeObject(mappedMessage);

                            if (await SqsRepository.PublishMessage(mappedMessageStr))
                            {
                                await rawMessage.AcknowledgeAsync();
                            }
                            else
                            {
                                FailedMessages++;
                                Logger.LogInformation($"There are {FailedMessages} failed messages.");
                            }
                        });

                        // Logger.LogInformation($"Processed message batch. There are now {consumer.UnconsumedMessageCount} unconsumed messages.");

                        processedMessages += 50;
                        totalMessages += 50;

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

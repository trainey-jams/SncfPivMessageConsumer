using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SncfPivMessageConsumer._OpenWire;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.Config.Openwire;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Channels
{
    public class ChannelProducer : IChannelProducer
    {
        private readonly IServiceContext ServiceContext;
        private readonly ILogger<ChannelProducer> Logger;
        private readonly SessionConfig SessionConfig;
        private readonly IOpenWireSessionFactory SessionFactory;

        public ChannelProducer(
            IServiceContext serviceContext,
            ILogger<ChannelProducer> logger,
            IOptions<SessionConfig> sessionConfig,
            IOpenWireSessionFactory sessionFactory)
        {
            ServiceContext = serviceContext;
            Logger = logger;
            SessionConfig = sessionConfig.Value;
            SessionFactory = sessionFactory;
        }

        public async Task WriteToChannel(ChannelWriter<ActiveMQMessageWrapper> channelWriter, CancellationToken token)
        {
            try
            {
                using var session = await SessionFactory.GetSession(SessionConfig.AcknowledgementMode);

                Logger.LogInformation("Connected to SNCF PIV message broker, session started.");

                using var dest = await session.GetTopicAsync(SessionConfig.TopicName);
                using MessageConsumer consumer = (MessageConsumer)await session.CreateConsumerAsync(dest);

                while (!token.IsCancellationRequested)
                {
                    while (await channelWriter.WaitToWriteAsync())
                    {
                        ActiveMQMessageWrapper rawMessage = new ActiveMQMessageWrapper
                        {
                            Message = await consumer.ReceiveAsync() as ActiveMQMessage,
                            ConversationId = ServiceContext.ConversationId,
                        };

                        await channelWriter.WriteAsync(rawMessage);
                    }
                }

                channelWriter.Complete();
            }
            catch (Exception ex)
            {
                Logger.LogError("Unexpected exception occurred. {Exception}", ex.Message);
            }
        }
    }
}

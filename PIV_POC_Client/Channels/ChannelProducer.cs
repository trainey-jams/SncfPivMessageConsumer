using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config.Openwire;
using System.Threading.Channels;

namespace PIV_POC_Client.Channels
{
    public class ChannelProducer : IChannelProducer
    {
        private readonly ILogger<ChannelProducer> Logger;
        private readonly SessionConfig SessionConfig;
        private readonly IOpenWireSessionFactory SessionFactory;

        public ChannelProducer(
            ILogger<ChannelProducer> logger,
            IOptions<SessionConfig> sessionConfig,
            IOpenWireSessionFactory sessionFactory)
        {
            Logger = logger;
            SessionConfig = sessionConfig.Value;
            SessionFactory = sessionFactory;
        }

        public async Task WriteToChannel(ChannelWriter<ActiveMQMessage> channelWriter, CancellationToken token)
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
                        var rawMessage = await consumer.ReceiveAsync() as ActiveMQMessage;

                        await channelWriter.WriteAsync(rawMessage);
                    }
                }

                channelWriter.Complete();
            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }   
        }
    }
}

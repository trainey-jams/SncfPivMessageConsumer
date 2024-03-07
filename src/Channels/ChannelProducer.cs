using System.Threading.Channels;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SncfPivMessageConsumer._OpenWire;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.Config.Openwire;

namespace SncfPivMessageConsumer.Channels;

public class ChannelProducer(IServiceContext serviceContext,
        ILogger<ChannelProducer> logger,
        IOptions<SessionConfig> sessionConfig,
        IOpenWireSessionFactory sessionFactory)
    : IChannelProducer
{
    private readonly SessionConfig SessionConfig = sessionConfig.Value;

    public async Task WriteToChannel(ChannelWriter<ActiveMQMessageWrapper> channelWriter, CancellationToken token)
    {
        try
        {
            using var session = await sessionFactory.GetSession(SessionConfig.AcknowledgementMode);

            logger.LogInformation("Connected to SNCF PIV message broker, session started.");

            using var dest = await session.GetTopicAsync(SessionConfig.TopicName);
            using MessageConsumer consumer = (MessageConsumer)await session.CreateConsumerAsync(dest);

            while (!token.IsCancellationRequested)
            {
                while (await channelWriter.WaitToWriteAsync())
                {
                    ActiveMQMessageWrapper rawMessage = new ActiveMQMessageWrapper
                    {
                        Message = await consumer.ReceiveAsync() as ActiveMQMessage,
                        ConversationId = serviceContext.ConversationId,
                    };

                    await channelWriter.WriteAsync(rawMessage);
                }
            }

            channelWriter.Complete();
        }
        catch (Exception ex)
        {
            // todo: better exception message, e.g. "Error while processing request from {Address}"
            logger.LogError(ex,"Unexpected exception occurred.");
        }
    }
}
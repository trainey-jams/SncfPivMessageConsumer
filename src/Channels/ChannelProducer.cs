using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.OpenWire;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Channels;

public class ChannelProducer : IChannelProducer
{
    private readonly IServiceContext ServiceContext;
    private readonly ILogger<ChannelProducer> Logger;
    private readonly IOpenWireConsumerFactory OpenWireConsumerFactory;

    public ChannelProducer(
        IServiceContext serviceContext,
        ILogger<ChannelProducer> logger,
        IOpenWireConsumerFactory openWireConsumerFactory)
    {
        ServiceContext = serviceContext;
        Logger = logger;
        OpenWireConsumerFactory = openWireConsumerFactory;
    }

    public async Task WriteToChannel(ChannelWriter<ActiveMQMessageWrapper> channelWriter, CancellationToken token)
    {
        try
        {
            using var consumer = await OpenWireConsumerFactory.GetMessageConsumer();

            while (await channelWriter.WaitToWriteAsync(token))
            {
                ActiveMQMessageWrapper rawMessage = new ActiveMQMessageWrapper
                {
                    Message = await consumer.ReceiveAsync() as ActiveMQMessage,
                    ConversationId = ServiceContext.ConversationId,
                };

                await channelWriter.WriteAsync(rawMessage);
            }
            
            channelWriter.Complete();
        }
        catch (Exception ex)
        {
            Logger.LogError("Unexpected exception occurred. {Exception}", ex.Message);
        }
    }
}  
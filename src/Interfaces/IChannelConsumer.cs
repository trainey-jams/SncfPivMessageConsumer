using Apache.NMS.ActiveMQ.Commands;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IChannelConsumer
    {
        Task ConsumeMessages(ChannelReader<ActiveMQMessage> channelReader, CancellationToken token);
    }
}

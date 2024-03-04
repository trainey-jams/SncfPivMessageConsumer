using Apache.NMS.ActiveMQ.Commands;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IChannelProducer
    {
        Task WriteToChannel(ChannelWriter<ActiveMQMessage> channelWriter, CancellationToken token);
    }
}

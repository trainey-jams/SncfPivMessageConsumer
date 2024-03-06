using Apache.NMS.ActiveMQ.Commands;
using SncfPivMessageConsumer.Models;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IChannelProducer
    {
        Task WriteToChannel(ChannelWriter<ActiveMQMessageWrapper> channelWriter, CancellationToken token);
    }
}

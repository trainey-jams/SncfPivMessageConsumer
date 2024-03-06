using Apache.NMS.ActiveMQ.Commands;
using SncfPivMessageConsumer.Models;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IChannelConsumer
    {
        Task ConsumeMessages(ChannelReader<ActiveMQMessageWrapper> channelReader, CancellationToken token);
    }
}

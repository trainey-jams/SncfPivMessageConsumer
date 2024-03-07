using System.Threading.Channels;
using SncfPivMessageConsumer.Models;

namespace SncfPivMessageConsumer.Channels;

public interface IChannelProducer
{
    Task WriteToChannel(ChannelWriter<ActiveMQMessageWrapper> channelWriter, CancellationToken token);
}
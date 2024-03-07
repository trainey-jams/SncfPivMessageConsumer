using System.Threading.Channels;
using SncfPivMessageConsumer.Models;

namespace SncfPivMessageConsumer.Channels;

public interface IChannelConsumer
{
    Task ConsumeMessages(ChannelReader<ActiveMQMessageWrapper> channelReader, CancellationToken token);
}
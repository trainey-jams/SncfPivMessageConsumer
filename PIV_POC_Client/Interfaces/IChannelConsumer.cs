using Apache.NMS.ActiveMQ.Commands;
using System.Threading.Channels;

namespace PIV_POC_Client.Interfaces
{
    public interface IChannelConsumer
    {
        Task ConsumeMessages(ChannelReader<ActiveMQMessage> channelReader, CancellationToken token);
    }
}

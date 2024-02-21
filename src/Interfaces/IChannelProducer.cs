using Apache.NMS.ActiveMQ.Commands;
using System.Threading.Channels;

namespace PIV_POC_Client.Interfaces
{
    public interface IChannelProducer
    {
        Task WriteToChannel(ChannelWriter<ActiveMQMessage> channelWriter, CancellationToken token);
    }
}

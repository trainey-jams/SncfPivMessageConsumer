using Apache.NMS.ActiveMQ.Commands;
using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.Interfaces
{
    public interface IActiveMQMapper
    {
        PivMessageRoot Map(ActiveMQMessage rawMessage);
    }
}

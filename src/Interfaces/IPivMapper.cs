using Apache.NMS.ActiveMQ.Commands;
using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.Interfaces
{
    public interface IPivMapper
    {
        PivMessageRoot MapAndTranslate(ActiveMQMessage rawMessage);

        string Serialize(object obj, bool useLongNames);
    }
}

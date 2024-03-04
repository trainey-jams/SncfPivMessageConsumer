using Apache.NMS.ActiveMQ.Commands;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IPivMapper
    {
        PivMessageRoot MapAndTranslate(ActiveMQMessage rawMessage);

        string Serialize(object obj, bool useLongNames);
    }
}

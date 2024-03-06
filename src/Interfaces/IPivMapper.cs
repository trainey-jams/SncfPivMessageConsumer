using Apache.NMS.ActiveMQ.Commands;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IPivMapper
    {
        public PivMessageRoot MapAndTranslate(ActiveMQMessageWrapper rawMessage);

        public string Serialize(object obj, bool useLongNames);
    }
}

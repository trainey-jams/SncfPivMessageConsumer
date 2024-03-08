using Apache.NMS;

namespace SncfPivMessageConsumer.OpenWire
{
    public interface IOpenWireConsumerFactory
    {
        Task<IMessageConsumer> GetMessageConsumer();
    }
}
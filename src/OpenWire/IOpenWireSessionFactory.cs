using Apache.NMS;

namespace SncfPivMessageConsumer.OpenWire
{
    public interface IOpenWireSessionFactory
    {
        Task<ISession> GetSession(AcknowledgementMode acknowledgementMode);
    }
}

using Apache.NMS;

namespace SncfPivMessageConsumer.Interfaces
{
    public interface IOpenWireConnectionFactory
    {
        Task<IConnection> GetConnection();
    }
}

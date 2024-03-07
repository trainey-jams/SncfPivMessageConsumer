using Apache.NMS;

namespace SncfPivMessageConsumer.OpenWire;

public interface IOpenWireConnectionFactory
{
    Task<IConnection> GetConnection();
}
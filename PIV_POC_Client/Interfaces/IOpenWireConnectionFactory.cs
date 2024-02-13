using Apache.NMS;

namespace PIV_POC_Client.Interfaces
{
    public interface IOpenWireConnectionFactory
    {
        Task<IConnection> GetConnection();
    }
}

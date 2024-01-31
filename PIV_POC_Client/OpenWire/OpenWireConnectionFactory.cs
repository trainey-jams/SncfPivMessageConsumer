using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config.Openwire;

namespace PIV_POC_Client._OpenWire
{

    public class OpenWireConnectionFactory : IOpenWireConnectionFactory
    {
        private readonly OpenWireConnectionConfig Config;

        public OpenWireConnectionFactory(IOptions<OpenWireConnectionConfig> config)
        {
            Config = config.Value;
        }

        public async Task<IConnection> GetConnection()
        {
            var factory = new ConnectionFactory
            {
                BrokerUri = new Uri(Config.BrokerUrl),
                UserName = Config.UserName,
                Password = Config.Password,
                ClientId = Config.ClientId
            };

            return await factory.CreateConnectionAsync();
        }
    }
}

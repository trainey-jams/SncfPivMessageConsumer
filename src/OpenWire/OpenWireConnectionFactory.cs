using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Microsoft.Extensions.Options;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Models.Config.Openwire;

namespace SncfPivMessageConsumer._OpenWire
{

    public class OpenWireConnectionFactory : IOpenWireConnectionFactory
    {
        private readonly BrokerConfig Config;

        public OpenWireConnectionFactory(IOptions<BrokerConfig> config)
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

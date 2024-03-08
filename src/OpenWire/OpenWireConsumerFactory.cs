using Apache.NMS;
using Microsoft.Extensions.Options;
using SncfPivMessageConsumer.Models.Config.Openwire;

namespace SncfPivMessageConsumer.OpenWire;

public class OpenWireConsumerFactory : IOpenWireConsumerFactory
{
    private readonly IOpenWireSessionFactory SessionFactory;
    private readonly SessionConfig SessionConfig;

    public OpenWireConsumerFactory(
        IOptions<SessionConfig> sessionConfig,
        IOpenWireSessionFactory sessionFactory)
    {
        SessionFactory = sessionFactory;
        SessionConfig = sessionConfig.Value;
    }

    public async Task<IMessageConsumer> GetMessageConsumer()
    {
        var session = await SessionFactory.GetSession(SessionConfig.AcknowledgementMode);

        var dest = await session.GetTopicAsync(SessionConfig.TopicName);

        return await session.CreateConsumerAsync(dest);
    }
}


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewRelic.Api.Agent;
using SncfPivMessageConsumer.AppComposition;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.Config;
using SncfPivMessageConsumer.Models.Config.Openwire;
using SncfPivMessageConsumer.OpenWire;
using System.Threading.Channels;
using Trainline.Extensions.Logging.Providers.Serilog.Extensions;

namespace SncfPivMessageConsumer.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTrainlineSerilogServices(configuration).BuildServiceProvider();

            services.AddSingleton<IAgent>(_ => NewRelic.Api.Agent.NewRelic.GetAgent());

            services.Configure<SnsConfig>(configuration.GetSection("SnsConfig"));
            services.Configure<BrokerConfig>(configuration.GetSection("BrokerConfig"));
            services.Configure<SessionConfig>(configuration.GetSection("SessionConfig"));
            services.Configure<MessageServiceConfig>(configuration.GetSection("MessageServiceConfig"));

            services.AddSingleton<IServiceContext, ServiceContext>();
            services.AddSingleton<Channel<ActiveMQMessageWrapper>>(Channel.CreateBounded<ActiveMQMessageWrapper>(500));

            services.AddTransient<IPivMapper, PivMapper>();
            services.AddTransient<IOpenWireConnectionFactory, OpenWireConnectionFactory>();
            services.AddTransient<IOpenWireSessionFactory, OpenWireSessionFactory>();
            services.AddTransient<IOpenWireConsumerFactory, OpenWireConsumerFactory>();

            services.AddAws();

            services.AddTransient<IChannelConsumer, ChannelConsumer>();
            services.AddTransient<IChannelProducer, ChannelProducer>();

            services.AddSingleton(configuration);

            return services;
        }
    }
}

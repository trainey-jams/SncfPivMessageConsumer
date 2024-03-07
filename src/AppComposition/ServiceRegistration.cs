using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewRelic.Api.Agent;
using SncfPivMessageConsumer._OpenWire;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.Config;
using SncfPivMessageConsumer.Models.Config.Openwire;
using SncfPivMessageConsumer.OpenWire;
using Trainline.Extensions.Logging.Providers.Serilog.Extensions;

namespace SncfPivMessageConsumer.AppComposition;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        var Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables().Build();

        services.AddTrainlineSerilogServices(Configuration).BuildServiceProvider();

        services.AddSingleton<IAgent>(_ => NewRelic.Api.Agent.NewRelic.GetAgent());

        services.Configure<SnsConfig>(Configuration.GetSection("SnsConfig"));
        services.Configure<BrokerConfig>(Configuration.GetSection("BrokerConfig"));
        services.Configure<SessionConfig>(Configuration.GetSection("SessionConfig"));

        services.AddSingleton<IServiceContext, ServiceContext>();
        services.AddSingleton<Channel<ActiveMQMessageWrapper>>(Channel.CreateBounded<ActiveMQMessageWrapper>(500));

        services.AddTransient<IPivMapper, PivMapper>();
        services.AddTransient<IOpenWireConnectionFactory, OpenWireConnectionFactory>();
        services.AddTransient<IOpenWireSessionFactory, OpenWireSessionFactory>();

        services.AddAws();

        services.AddTransient<IChannelConsumer, ChannelConsumer>();
        services.AddTransient<IChannelProducer, ChannelProducer>();

        services.AddSingleton(Configuration);

        return services;
    }
}
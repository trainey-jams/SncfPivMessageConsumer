using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using Serilog;
using SncfPivMessageConsumer._OpenWire;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.Config;
using SncfPivMessageConsumer.Models.Config.Openwire;
using System.Threading.Channels;
using Trainline.Extensions.Logging.Providers.Serilog.Extensions;

namespace SncfPivMessageConsumer.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

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
}

﻿using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.Channels;
using PIV_POC_Client.DI;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Mappers;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.Openwire;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            IConfigurationRoot Configuration;
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddTrainlineLogging(Configuration).BuildServiceProvider();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.Configure<SnsConfig>(Configuration.GetSection("SnsConfig"));
            services.Configure<BrokerConfig>(Configuration.GetSection("BrokerConfig"));
            services.Configure<SessionConfig>(Configuration.GetSection("SessionConfig"));

            services.AddSingleton<Channel<ActiveMQMessage>>(Channel.CreateBounded<ActiveMQMessage>(500));

            services.AddTransient<IActiveMQMapper, ActiveMQMapper>();
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

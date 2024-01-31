using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIV_POC_Client.AWS.SQS;
using PIV_POC_Client.DAL.Repositories;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.DAL;
using PIV_POC_Client.Processor;
using PIV_POC_Client.Services;
using PIV_POC_Client.STOMP.Wrappers;
using PIV_POC_Client.WebSocketClient;

namespace PIV_POC_Client.App
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();
                ConfigureServices(services);

                var serviceProvider = services.BuildServiceProvider();

                var service = serviceProvider.GetService<PIVNotificationClient>();

                await service.GetMessages();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot Configuration;
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.Configure<NotificationClientConfiguration>(Configuration.GetSection("NotificationClientConfiguration"));
            services.Configure<SqlConnectionConfiguration>(Configuration.GetSection("SqlConnectionConfiguration"));
            services.Configure<MessageRepositoryConfiguration>(Configuration.GetSection("MessageRepositoryConfiguration"));
            services.Configure<MessageProcessorConfiguration>(Configuration.GetSection("MessageProcessorConfiguration"));
            services.Configure<SqsConfig>(Configuration.GetSection("SqsConfig"));


            services.AddTransient<IWebSocketClientFactory, WebSocketClientFactory>();
            services.AddTransient<IMessageProcessor, MessageProcessor>();
            services.AddTransient<IStompClientFrameWrapper, StompClientFrameWrapper>();
            services.AddTransient<IStompServerFrameWrapper, StompServerFrameWrapper>();

            services.AddTransient<ISqsClientFactory, SqsClientFactory>();

            services.AddTransient<ISqsRepository, SqsRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton(Configuration);
            services.AddSingleton<PIVNotificationClient>();
        }
    }
}

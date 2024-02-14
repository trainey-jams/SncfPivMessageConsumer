using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.AWS.Repos;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Mappers;
using PIV_POC_Client.Publishers;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.Openwire;
using PIV_POC_Client.Services;

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

                var service = serviceProvider.GetService<MessageClient>();

                await service.EstablishPivBrokerConnection();
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

            services.AddTrainlineLogging(Configuration).BuildServiceProvider();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.Configure<SqsConfig>(Configuration.GetSection("SqsConfig"));
            services.Configure<S3Config>(Configuration.GetSection("S3Config"));
            services.Configure<BrokerConfig>(Configuration.GetSection("BrokerConfig"));
            services.Configure<SessionConfig>(Configuration.GetSection("SessionConfig"));
            services.Configure<MessageServiceConfig>(Configuration.GetSection("MessageServiceConfig"));

            services.AddTransient<IActiveMQMapper, ActiveMQMapper>();

            services.AddTransient<IOpenWireConnectionFactory, OpenWireConnectionFactory>();
            services.AddTransient<IOpenWireSessionFactory, OpenWireSessionFactory>();

            services.AddTransient(sp => new AWSOptions
            {
                Credentials = new CredentialProfileStoreChain().TryGetAWSCredentials("default", out var defaultCredentials)
                ? defaultCredentials
                : new InstanceProfileAWSCredentials(),
                Region = RegionEndpoint.EUWest1
            });

            services.AddAWSService<IAmazonSQS>();
            services.AddTransient<ISqsRepository, SqsRepository>();
            
            services.AddAWSService<IAmazonS3>();
            services.AddTransient<IS3Repository, S3Repository>();

            services.AddTransient<IMessagePublisher, MessagePublisher>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton(Configuration);
            services.AddSingleton<MessageClient>();
        }
    }
}

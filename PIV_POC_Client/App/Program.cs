using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIV_POC_Client._OpenWire;
using PIV_POC_Client.AWS.Repos;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.IOC;
using PIV_POC_Client.Mappers;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.AWS;
using PIV_POC_Client.Models.Config.Openwire;
using PIV_POC_Client.Processor;
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
            
            services.Configure<AWSCredentialConfig>(Configuration.GetSection("AWSCredentialConfig"));
            services.Configure<SqsConfig>(Configuration.GetSection("SqsConfig"));

            services.Configure<OpenWireConnectionConfig>(Configuration.GetSection("OpenWireConnectionConfig"));

            services.AddTransient<IMessageProcessor, MessageProcessor>();
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
            services.RegisterDynamoDb(Configuration);

            services.AddTransient<IDynamoDbRepository, DynamoDbRepository>();
            services.AddTransient<ISqsRepository, SqsRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton(Configuration);
            services.AddSingleton<PIVNotificationClient>();
        }
    }
}

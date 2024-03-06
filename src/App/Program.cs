using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SncfPivMessageConsumer.DI;

namespace SncfPivMessageConsumer.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostContext, builder) =>
                {
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                    else
                    {
                        builder.AddAwsSecrets();
                    }
                }).ConfigureServices(services =>
                {
                    ServiceRegistration.ConfigureServices(services);
                    services.AddHostedService<MessageService>();
                }).Build();
                
                await host.RunAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

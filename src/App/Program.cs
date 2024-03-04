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
                var builder = Host.CreateApplicationBuilder(args);

                builder.Services.ConfigureServices();

                builder.Services.AddHostedService<MessageService>();
                IHost host = builder.Build();
                await host.RunAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PIV_POC_Client.Services;
using SncfPivMessageConsumer.DI;

namespace PIV_POC_Client.App
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
                host.Run();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

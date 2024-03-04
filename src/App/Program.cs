using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SncfPivMessageConsumer.Services;
using SncfPivMessageConsumer.DI;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer._OpenWire;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models.Config;
using SncfPivMessageConsumer.Models.Config.Openwire;
using SncfPivMessageConsumer.App;
using SncfPivMessageConsumer.DI;
using System.Threading.Channels;

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
                host.Run();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

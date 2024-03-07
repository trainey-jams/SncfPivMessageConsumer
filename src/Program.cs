using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SncfPivMessageConsumer.App;
using SncfPivMessageConsumer.AppComposition;

namespace SncfPivMessageConsumer;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = Host.CreateApplicationBuilder();

            builder.Services.ConfigureServices();

            if (!builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddAwsSecrets(opt => opt.SecretName = "dev/SncfPivMessageConsumer");
            }

            builder.Services.AddHostedService<MessageService>();

            var host = builder.Build();

            await host.RunAsync();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
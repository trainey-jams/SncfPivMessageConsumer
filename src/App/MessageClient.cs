using Microsoft.Extensions.Logging;
using PIV_POC_Client.Interfaces;

namespace PIV_POC_Client.App
{
    public class MessageClient
    {
        private readonly ILogger<MessageClient> Logger;
        private readonly IMessageService MessageService;

        public MessageClient(
            ILogger<MessageClient> logger,  
            IMessageService messageService)
        {
            Logger = logger;
            MessageService = messageService;
        }

        public async Task Run()
        {
            var cancellationToken = new CancellationTokenSource();
            var token = cancellationToken.Token;

            await MessageService.ProcessMessages(token);

            Console.ReadKey();
            
            cancellationToken.Cancel();

            if (token.IsCancellationRequested)
            {
                Logger.LogInformation("Cancellation has been requested, disconnecting from broker and shutting down.");

                await Task.Delay(5000);
            }
        }
    }
}

using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PIV_POC_Client.Interfaces;
using System.Threading.Channels;

namespace PIV_POC_Client.Services
{
    public class MessageService : BackgroundService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly IChannelProducer ChannelProducer;
        private readonly IChannelConsumer ChannelConsumer;
        private readonly Channel<ActiveMQMessage> Channel;

        public MessageService(
            ILogger<MessageService> logger,
            IChannelProducer channelProducer,
            IChannelConsumer channelConsumer,
            Channel<ActiveMQMessage> channel
            )
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ChannelProducer = channelProducer ?? throw new ArgumentNullException(nameof(channelProducer));
            ChannelConsumer = channelConsumer ?? throw new ArgumentNullException(nameof(channelConsumer));
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.WhenAll
                        (
                            ChannelProducer.WriteToChannel(Channel.Writer, cancellationToken), 
                            Parallel.ForAsync(0, 100, async (item, cancellationToken) =>
                            {
                                await ChannelConsumer.ConsumeMessages(Channel.Reader, cancellationToken);
                            })
                        );                              

                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }
                }
            }, cancellationToken);

            //Logger.LogInformation("Cancellation has been requested, disconnecting from broker and shutting down.");
        }
    }
}

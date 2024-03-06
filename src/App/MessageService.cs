using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Models;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.App
{
    public class MessageService : BackgroundService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly IChannelProducer ChannelProducer;
        private readonly IChannelConsumer ChannelConsumer;
        private readonly Channel<ActiveMQMessageWrapper> Channel;

        public MessageService(
            ILogger<MessageService> logger,
            IChannelProducer channelProducer,
            IChannelConsumer channelConsumer,
            Channel<ActiveMQMessageWrapper> channel
            )
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ChannelProducer = channelProducer ?? throw new ArgumentNullException(nameof(channelProducer));
            ChannelConsumer = channelConsumer ?? throw new ArgumentNullException(nameof(channelConsumer));
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
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
                Logger.LogError("Unexpected exception occurred. {Exception}", ex.Message);
            }
        }
    }
}

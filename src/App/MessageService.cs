using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using PIV_POC_Client.Interfaces;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.App
{
    public class MessageService : IMessageService
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

        public async Task ProcessMessages(CancellationToken cancellationToken)
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
        }
    }
}

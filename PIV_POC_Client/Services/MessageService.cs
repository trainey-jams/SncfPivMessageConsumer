using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Channels;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using System.Threading.Channels;

namespace PIV_POC_Client.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly MessageServiceConfig ServiceConfig;
        private readonly IChannelProducer ChannelProducer;
        private readonly IChannelConsumer ChannelConsumer;
        private readonly Channel<ActiveMQMessage> Channel;

        public MessageService(
            ILogger<MessageService> logger,
            IOptions<MessageServiceConfig> serviceConfig,
            IChannelProducer channelProducer,
            IChannelConsumer channelConsumer,
            Channel<ActiveMQMessage> channel
            )
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceConfig = serviceConfig.Value ?? throw new ArgumentNullException(nameof(serviceConfig));
            ChannelProducer = channelProducer ?? throw new ArgumentNullException(nameof(channelProducer));
            ChannelConsumer = channelConsumer ?? throw new ArgumentNullException(nameof(channelConsumer));
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public async Task ProcessPIVMessages(CancellationToken cancellationToken)
        {
            var task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await ChannelProducer.WriteToChannel(Channel.Writer, cancellationToken);

                        await Parallel.ForEachAsync(Channel.Reader.ReadAllAsync(), async (item, cancellationToken) =>
                        {
                            await ChannelConsumer.ConsumeMessages(Channel.Reader, cancellationToken);
                        });
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

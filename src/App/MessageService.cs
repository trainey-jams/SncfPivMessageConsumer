using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.Models;
using System.Threading.Channels;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Models.Config;
using Microsoft.Extensions.Options;

namespace SncfPivMessageConsumer.App
{
    public class MessageService : BackgroundService
    {
        private readonly ILogger<MessageService> Logger;
        private readonly MessageServiceConfig MessageServiceConfig;
        private readonly IChannelProducer ChannelProducer;
        private readonly IChannelConsumer ChannelConsumer;
        private readonly Channel<ActiveMQMessageWrapper> Channel;
       
        public MessageService(
            ILogger<MessageService> logger,
            IOptions<MessageServiceConfig> messageServiceConfig,
            IChannelProducer channelProducer,
            IChannelConsumer channelConsumer,
            Channel<ActiveMQMessageWrapper> channel
            )
        {
            Logger = logger;
            MessageServiceConfig = messageServiceConfig.Value;
            ChannelProducer = channelProducer;
            ChannelConsumer = channelConsumer;
            Channel = channel;

        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            Logger.LogInformation("Starting message service.");

            try
            {
                await Task.WhenAll(
                    ChannelProducer.WriteToChannel(Channel.Writer, cancellationToken),
                    ChannelConsumer.ConsumeMessages(Channel.Reader, cancellationToken));
            }
            catch(OperationCanceledException canEx)
            {
                Logger.LogError("Cancellation has been requested {info}", canEx.Message);

                Logger.LogInformation("Message service shutting down.");
            }

            catch(Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}

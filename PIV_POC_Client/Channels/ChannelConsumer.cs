using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.PivMessage.Root;
using PIV_POC_Client.Services;
using PIV_POC_Client.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PIV_POC_Client.Channels
{
    public interface IChannelConsumer
    {
        Task ConsumeMessages(ChannelReader<ActiveMQMessage> channelReader, CancellationToken token);
    }

    public class ChannelConsumer : IChannelConsumer
    {
        private readonly ILogger<ChannelConsumer> Logger;
        private readonly IActiveMQMapper Mapper;
        private readonly IMessagePublisher MessagePublisher;

        public ChannelConsumer(
            ILogger<ChannelConsumer> logger,
            IActiveMQMapper mapper,
            IMessagePublisher messagePublisher)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            MessagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task ConsumeMessages(ChannelReader<ActiveMQMessage> channelReader, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (await channelReader.WaitToReadAsync())
                {
                    var rawMessage = await channelReader.ReadAsync();

                    PivMessageRoot mappedMessage = Mapper.Map(rawMessage);

                    string messageStr = TranslationSerializer.Serialize(mappedMessage, true);

                    //not sure what we agreed for message keys.
                    string messageKey = $"{mappedMessage.MessageId}{mappedMessage.BrokerOutTime}";

                    if (await MessagePublisher.PublishMessage(messageKey, messageStr))
                    {
                        await rawMessage.AcknowledgeAsync();
                    }
                    else
                    {
                        Logger.LogWarning($"Failed to process message with key: {messageKey}");
                    }
                }
            }
        }
    }
}

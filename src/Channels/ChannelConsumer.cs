using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;
using System.Threading.Channels;

namespace SncfPivMessageConsumer.Channels
{
    public class ChannelConsumer : IChannelConsumer
    {
        private readonly ILogger<ChannelConsumer> Logger;
        private readonly IPivMapper Mapper;
        private readonly ISnsRepository SnsRepository;

        public ChannelConsumer(
            ILogger<ChannelConsumer> logger,
            IPivMapper mapper,
            ISnsRepository snsRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SnsRepository = snsRepository ?? throw new ArgumentNullException(nameof(snsRepository));
        }

        public async Task ConsumeMessages(ChannelReader<ActiveMQMessageWrapper> channelReader, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (await channelReader.WaitToReadAsync())
                {
                    try
                    {
                        var rawMessage = await channelReader.ReadAsync();

                        PivMessageRoot mappedMessage = Mapper.MapAndTranslate(rawMessage);

                        string messageStr = Mapper.Serialize(mappedMessage, true);

                        if (await SnsRepository.PublishMessage(messageStr))
                        {
                            await rawMessage.Message.AcknowledgeAsync();
                        }
                        else
                        {
                            Logger.LogWarning("Failed to publish message to SNS. MessageId {MessageId}, ConversationId {ConversationId}", mappedMessage.MessageId, mappedMessage.ConversationId);
                        }
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError("Unexpected exception occurred. {Exception}", ex.Message);
                    }
                }
            }
        }
    }
}

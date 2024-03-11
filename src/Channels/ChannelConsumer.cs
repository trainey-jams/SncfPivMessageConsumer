using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.AWS.Repos;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Channels;

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
        Logger = logger;
        Mapper = mapper;
        SnsRepository = snsRepository;
    }

    public async Task ConsumeMessages(ChannelReader<ActiveMQMessageWrapper> channelReader, CancellationToken token)
    {
        while (await channelReader.WaitToReadAsync(token))
        {
            try
            {
                var rawMessage = await channelReader.ReadAsync();

                PivMessageRoot mappedMessage = await Mapper.MapAndTranslate(rawMessage);

                string messageStr = Mapper.Serialize(mappedMessage, true);

                if (await SnsRepository.PublishMessage(messageStr))
                {
                    await rawMessage.Message.AcknowledgeAsync();
                }
                else
                {
                    Logger.LogWarning(
                        "Failed to publish message to SNS. MessageId {MessageId}, ConversationId {ConversationId}",
                        mappedMessage.MessageId, mappedMessage.ConversationId);
                }
            }

            catch (Exception ex)
            {
                Logger.LogError("Unexpected exception occurred. {Exception}", ex.Message);
            }
        }
    }
}
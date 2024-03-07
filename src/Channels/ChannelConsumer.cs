﻿using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using SncfPivMessageConsumer.AWS.Repos;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Channels;

public class ChannelConsumer(ILogger<ChannelConsumer> logger,
        IPivMapper mapper,
        ISnsRepository snsRepository)
    : IChannelConsumer
{
    private readonly ILogger<ChannelConsumer> Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPivMapper Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ISnsRepository SnsRepository = snsRepository ?? throw new ArgumentNullException(nameof(snsRepository));

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
}
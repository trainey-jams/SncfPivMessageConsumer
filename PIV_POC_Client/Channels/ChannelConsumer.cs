using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.PivMessage.Root;
using PIV_POC_Client.Utility;
using System.Threading.Channels;

namespace PIV_POC_Client.Channels
{
    public class ChannelConsumer : IChannelConsumer
    {
        private readonly ILogger<ChannelConsumer> Logger;
        private readonly IActiveMQMapper Mapper;
        private readonly ISnsRepository SnsRepository;

        public ChannelConsumer(
            ILogger<ChannelConsumer> logger,
            IActiveMQMapper mapper,
            ISnsRepository snsRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SnsRepository = snsRepository ?? throw new ArgumentNullException(nameof(snsRepository));
        }

        public async Task ConsumeMessages(ChannelReader<ActiveMQMessage> channelReader, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                while (await channelReader.WaitToReadAsync())
                {
                    try
                    {
                        var rawMessage = await channelReader.ReadAsync();

                        PivMessageRoot mappedMessage = Mapper.Map(rawMessage);

                        string messageStr = TranslationSerializer.Serialize(mappedMessage, true);

                        if (await SnsRepository.PublishMessage(messageStr))
                        {
                            await rawMessage.AcknowledgeAsync();
                        }
                        else
                        {
                            Logger.LogWarning($"Failed to process message with id {mappedMessage.MessageId}");
                        }
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }
                }
            }
        }
    }
}

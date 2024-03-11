using System.Text;
using Newtonsoft.Json;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Mappers;

public class PivMapper : IPivMapper
{
    public Task<PivMessageRoot> MapAndTranslate(ActiveMQMessageWrapper rawMessage)
    {
        PivMessageRoot root = new PivMessageRoot();

        root.Priority = rawMessage.Message.Priority;
        root.BrokerInTime = DateTimeOffset.FromUnixTimeMilliseconds(rawMessage.Message.BrokerInTime).LocalDateTime;
        root.BrokerOutTime = DateTimeOffset.FromUnixTimeMilliseconds(rawMessage.Message.BrokerOutTime).LocalDateTime;
        root.Expiration = rawMessage.Message.Expiration.ToString();
     //   root.MessageId = rawMessage.Message.MessageId.ToString();
        root.ConversationId = rawMessage.ConversationId;

        string payload = Encoding.UTF8.GetString(rawMessage.Message.Content);
        root.MessageBody = JsonConvert.DeserializeObject<MessageBody>(payload);

        return Task.FromResult(root);
    }

    public string Serialize(object obj, bool useLongNames)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.None;
        if (useLongNames)
        {
            settings.ContractResolver = new PivContractResolver();
        }

        return JsonConvert.SerializeObject(obj, settings);
    }
}
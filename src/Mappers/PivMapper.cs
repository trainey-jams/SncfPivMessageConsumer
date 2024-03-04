using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using SncfPivMessageConsumer.Models.PivMessage.Root;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Mappers;

namespace SncfPivMessageConsumer.Mappers
{
    public class PivMapper : IPivMapper
    {
        public PivMessageRoot MapAndTranslate(ActiveMQMessage rawMessage)
        {
            PivMessageRoot root = new PivMessageRoot();

            root.Priority = rawMessage.Priority;
            root.BrokerInTime = DateTimeOffset.FromUnixTimeMilliseconds(rawMessage.BrokerInTime).LocalDateTime;
            root.BrokerOutTime = DateTimeOffset.FromUnixTimeMilliseconds(rawMessage.BrokerOutTime).LocalDateTime;
            root.Expiration = rawMessage.Expiration.ToString();
            root.Destination = rawMessage.Destination.ToString();
            root.MessageId = rawMessage.MessageId.ToString();

            string payload = System.Text.Encoding.UTF8.GetString(rawMessage.Content);
            root.MessageBody = JsonConvert.DeserializeObject<MessageBody>(payload);

            return root;
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
}

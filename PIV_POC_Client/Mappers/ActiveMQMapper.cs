using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using PIV_POC_Client.Models.PivMessage.Root;
using PIV_POC_Client.Interfaces;

namespace PIV_POC_Client.Mappers
{

    public class ActiveMQMapper : IActiveMQMapper
    {
        public PivMessageRoot Map(ActiveMQMessage rawMessage)
        {
            PivMessageRoot root = new PivMessageRoot();

            root.Priority = rawMessage.Priority;
            root.BrokerInTime = rawMessage.BrokerInTime;
            root.Expiration = rawMessage.Expiration.ToString();
            root.Destination = rawMessage.Destination.ToString();
            root.PartitionKey = rawMessage.MessageId.ToString();

            string payload = System.Text.Encoding.UTF8.GetString(rawMessage.Content);
            root.MessageBody = JsonConvert.DeserializeObject<MessageBody>(payload);

            return root;
        }

        public PivMessageRoot MapToDTO(ActiveMQMessage rawMessage)
        {
            PivMessageRoot root = new PivMessageRoot();

            root.Priority = rawMessage.Priority;
            root.BrokerInTime = rawMessage.BrokerInTime;
            root.Expiration = rawMessage.Expiration.ToString();
            root.Destination = rawMessage.Destination.ToString();
            root.PartitionKey = rawMessage.MessageId.ToString();

            string payload = System.Text.Encoding.UTF8.GetString(rawMessage.Content);
            root.MessageBody = JsonConvert.DeserializeObject<MessageBody>(payload);

            return root;
        }
    }
}

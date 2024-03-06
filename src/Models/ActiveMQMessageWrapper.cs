using Apache.NMS.ActiveMQ.Commands;

namespace SncfPivMessageConsumer.Models
{
    public class ActiveMQMessageWrapper
    {
        public ActiveMQMessage Message { get; set; }

        public string ConversationId { get; set; } = string.Empty;
    }
}

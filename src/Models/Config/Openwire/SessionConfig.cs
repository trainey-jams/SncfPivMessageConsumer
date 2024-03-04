using Apache.NMS;

namespace SncfPivMessageConsumer.Models.Config.Openwire
{
    public class SessionConfig
    {
        public AcknowledgementMode AcknowledgementMode { get; set; }    

        public string TopicName { get; set; }  = string.Empty;
    }
}

using Apache.NMS;

namespace PIV_POC_Client.Models.Config.Openwire
{
    public class SessionConfig
    {
        public AcknowledgementMode AcknowledgementMode { get; set; }    

        public string TopicName { get; set; }  = string.Empty;
    }
}

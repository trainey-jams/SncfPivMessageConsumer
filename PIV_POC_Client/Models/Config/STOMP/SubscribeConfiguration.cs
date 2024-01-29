using PIV_POC_Client.Models.Enums;

namespace PIV_POC_Client.Models.Config.STOMP
{
    public class SubscribeConfiguration
    {
        public string Destination { get; set; } = string.Empty;

        public Guid SubscriptionId { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public string SubscriptionName {  get; set; } = string.Empty;

        //todo make private after initialization
        public AcknowledgementMode AcknowledgementMode { get; set; }

        public Dictionary<string, string> Selectors { get; set; } = new Dictionary<string, string>();

        public string GetAckowledgementMode()
        {
            if (AcknowledgementMode == AcknowledgementMode.Client)
                return "client";

            if (AcknowledgementMode == AcknowledgementMode.ClientIndividual)
                return "client-individual";

            else return "auto";
        }
    }
}

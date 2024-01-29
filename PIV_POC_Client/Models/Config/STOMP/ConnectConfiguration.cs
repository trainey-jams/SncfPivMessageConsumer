namespace PIV_POC_Client.Models.Config.STOMP
{
    public class ConnectConfiguration
    {
        public string Login { get; set; } = string.Empty;

        public string Passcode { get; set; } = string.Empty;

        public string AcceptVersion {  get; set; } = string.Empty;

        public Guid SubscriptionId { get; set; }

        public int IncomingHeartbeatMs { get; set; } = 0;

        public int OutgoingHeartbeatMs { get; set; } = 0;

        public Guid ClientId { get; set; }
    }
}

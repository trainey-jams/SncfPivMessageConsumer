namespace PIV_POC_Client.Models.Config.Websocket
{
    public class WebsocketClientConfiguration
    {
        public string Uri { get; set; } = string.Empty;

        public string SubProtocol { get; set; } = string.Empty;

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}

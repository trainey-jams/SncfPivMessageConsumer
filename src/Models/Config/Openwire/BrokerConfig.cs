namespace SncfPivMessageConsumer.Models.Config.Openwire;

// Todo: convert to record
public class BrokerConfig
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string BrokerUrl { get; set; } = string.Empty;
}
using System.Net.WebSockets;

namespace PIV_POC_Client.Interfaces
{
    public interface IMessageService
    {
        Task ProcessMessage(ClientWebSocket client, Guid subscriptionId);
    }
}

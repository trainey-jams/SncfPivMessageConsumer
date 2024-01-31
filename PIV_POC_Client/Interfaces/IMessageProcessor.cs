
namespace PIV_POC_Client.Interfaces
{
    public interface IMessageProcessor
    {
        Task<string> Process(Guid subscriptionId, string rawMessage);
    }
}
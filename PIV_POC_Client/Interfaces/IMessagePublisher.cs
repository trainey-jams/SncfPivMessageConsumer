
namespace PIV_POC_Client.Interfaces
{
    public interface IMessagePublisher
    {
        Task<bool> PublishMessage(string messageKey, string message);
    }
}
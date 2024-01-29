using PIV_POC_Client.Models.Results;

namespace PIV_POC_Client.Interfaces
{
    public interface IMessageProcessor
    {
        public Task Process(Guid subscriptionId, string rawMessage);
    }
}
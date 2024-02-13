using Amazon.SQS.Model;

namespace PIV_POC_Client.Interfaces
{
    public interface ISqsRepository
    {
        Task <bool>PublishMessage(string message);
    }
}
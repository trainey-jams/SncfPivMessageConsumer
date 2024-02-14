namespace PIV_POC_Client.Interfaces
{
    public interface IS3Repository
    {
        Task<bool> PublishMessage(string messageKey, string message);
    }
}

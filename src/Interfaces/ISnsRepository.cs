namespace PIV_POC_Client.Interfaces
{
    public interface ISnsRepository
    {
        Task<bool> PublishMessage(string message);
    }
}
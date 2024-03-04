namespace SncfPivMessageConsumer.Interfaces
{
    public interface ISnsRepository
    {
        Task<bool> PublishMessage(string message);
    }
}
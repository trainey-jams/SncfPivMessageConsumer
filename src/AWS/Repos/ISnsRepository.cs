namespace SncfPivMessageConsumer.AWS.Repos;

public interface ISnsRepository
{
    Task<bool> PublishMessage(string message);
}
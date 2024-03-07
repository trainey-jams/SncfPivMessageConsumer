namespace SncfPivMessageConsumer.Models;

public interface IServiceContext
{
    string ConversationId { get; }
}

public class ServiceContext : IServiceContext
{
    public string ConversationId => $"{Guid.NewGuid()}";
}
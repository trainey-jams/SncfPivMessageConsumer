using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.AWS.Repos
{
    public interface IDynamoDbRepository
    {
        public Task<bool> SaveAsync(PivMessageRoot messageRoot);
    }
}

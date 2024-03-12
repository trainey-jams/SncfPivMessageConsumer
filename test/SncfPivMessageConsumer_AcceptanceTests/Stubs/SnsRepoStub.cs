using SncfPivMessageConsumer.AWS.Repos;

namespace SncfPivMessageConsumer_AcceptanceTests.Stubs;

public class SnsRepoStub: ISnsRepository
{
    public Task<bool> PublishMessage(string message)
    {
        if (true)
        {
            return Task.FromResult(true);
        }
    }
}

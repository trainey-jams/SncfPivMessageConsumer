using SncfPivMessageConsumer.AWS.Repos;

namespace SncfPivMessageConsumer_AcceptanceTests.Stubs;

public class SnsRepoStub: ISnsRepository
{
    public async Task<bool> PublishMessage(string message)
    {
        if (message == "something")
        {
            return true;
        }


        return false;
    }
}

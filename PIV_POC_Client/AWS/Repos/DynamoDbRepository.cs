using Amazon.DynamoDBv2.DataModel;
using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.AWS.Repos
{
    public interface IDynamoDbRepository
    {
        public Task<bool> SaveAsync(PivMessageRoot messageRoot);
    }

    public class DynamoDbRepository : IDynamoDbRepository
    {
        private readonly DynamoDBOperationConfig Config;
        private readonly IDynamoDBContext Context;

        public DynamoDbRepository(IDynamoDBContext context, DynamoDBOperationConfig config)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Config = config;
        }

        public async Task<bool> SaveAsync(PivMessageRoot messageRoot)
        {
            try
            {
                await Context.SaveAsync(messageRoot, Config);

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }
    }
}

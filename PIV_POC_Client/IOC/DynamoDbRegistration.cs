using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIV_POC_Client.Models.Config.AWS;

namespace PIV_POC_Client.IOC
{
    internal static class DynamoDbRegistration
    {
        internal static IServiceCollection RegisterDynamoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AWSCredentialConfig settings = configuration.GetSection("AWSCredentialConfig").Get<AWSCredentialConfig>(); 

            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.EUWest1
            };

            serviceCollection.AddSingleton(serviceProvider =>
            {
                return new DynamoDBOperationConfig
                {
                    OverrideTableName = settings.TableName
                    
                };
            });

            serviceCollection.AddSingleton<IAmazonDynamoDB>(serviceProvider =>
            {
                if (settings.LocalMode)
                {
                    var dummyCredentials = new SessionAWSCredentials(settings.AccessKey,settings.SecretKey,settings.SessionToken);
                    return new AmazonDynamoDBClient(dummyCredentials,config);
                }

                return new AmazonDynamoDBClient();
            });

            serviceCollection.AddSingleton<IDynamoDBContext, DynamoDBContext>(serviceProvider =>
                new DynamoDBContext(serviceProvider.GetRequiredService<IAmazonDynamoDB>()));

            return serviceCollection;
        }
    }
}

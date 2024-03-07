using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SncfPivMessageConsumer.AWS.Repos;

namespace SncfPivMessageConsumer.AppComposition;

public static class AwsRegistration
{
    public static IServiceCollection AddAws(this IServiceCollection services)
    {
        services.AddTransient(sp => new AWSOptions
        {
            Credentials = new CredentialProfileStoreChain().TryGetAWSCredentials("default", out var defaultCredentials)
                ? defaultCredentials
                : new InstanceProfileAWSCredentials(),
            Region = RegionEndpoint.EUWest1
        });

        var circuitBreakerPolicy = Policy
            .Handle<AmazonS3Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

        var retryPolicy = Policy
            .Handle<AmazonS3Exception>()
            .RetryAsync(1);

        var policy = Policy.WrapAsync(circuitBreakerPolicy, retryPolicy);

        services.AddSingleton<IAsyncPolicy>(policy);

        services.AddAWSService<IAmazonSimpleNotificationService>();
        services.AddTransient<ISnsRepository, SnsRepository>();

        return services;
    }
}
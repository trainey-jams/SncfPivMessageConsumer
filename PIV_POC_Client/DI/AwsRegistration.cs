using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using PIV_POC_Client.AWS.Repos;
using PIV_POC_Client.Interfaces;
using Polly;

namespace PIV_POC_Client.DI
{
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

            services.AddAWSService<IAmazonSQS>();
            services.AddTransient<ISqsRepository, SqsRepository>();

            services.AddAWSService<IAmazonS3>();
            services.AddTransient<IS3Repository, S3Repository>();

            return services;
        }
    }
}

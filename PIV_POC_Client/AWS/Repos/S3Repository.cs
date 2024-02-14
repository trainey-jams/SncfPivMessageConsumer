using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using Polly;
using Polly.CircuitBreaker;
using System.Text;

namespace PIV_POC_Client.AWS.Repos
{
    public class S3Repository : IS3Repository
    {
        private readonly ILogger<S3Repository> Logger;
        private readonly S3Config S3Config;
        private readonly IAmazonS3 S3Client;
        private readonly IAsyncPolicy Policy;

        public S3Repository(ILogger<S3Repository> logger, IOptions<S3Config> s3Config,IAmazonS3 s3Client, IAsyncPolicy policy)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            S3Config = s3Config.Value;
            S3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));   
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public async Task<bool> PublishMessage(string messageKey, string message)
        {
            try
            {
                var response = await Policy.ExecuteAsync(async() => await Upload(Encoding.UTF8.GetBytes(message), messageKey));

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }

                Logger.LogWarning($"Could not upload message {messageKey} to S3 bucket {S3Config.S3BucketName}.");

                return false;
            }

            catch(AmazonS3Exception ex)
            {
                Logger.LogError(ex, $"Failed to write to S3, key: {messageKey} in bucket {S3Config.S3BucketName}");
                return false;
            }

            catch (BrokenCircuitException ex)
            {
                Logger.LogError(ex, "S3 circuit open");
                return false;
            }
        }

        private async Task<PutObjectResponse> Upload(byte[] byteArray, string messageKey)
        {
            await using var stream = new MemoryStream(byteArray);

            var request = new PutObjectRequest
            {
                BucketName = S3Config.S3BucketName,
                Key = messageKey,
                InputStream = stream,
                ContentType = S3Config.ContentType,
            };

            return await S3Client.PutObjectAsync(request);
        }
    }
}

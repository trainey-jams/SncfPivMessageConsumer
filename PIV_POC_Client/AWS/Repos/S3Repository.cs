using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS.ExtendedClient;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PIV_POC_Client.AWS.Repos
{
    public class S3Repository : IS3Repository
    {
        private readonly ILogger<S3Repository> Logger;
        private readonly IAmazonS3 S3Client;
        private readonly S3Config S3Config;

        public S3Repository(ILogger<S3Repository> logger, IOptions<S3Config> s3Config,IAmazonS3 s3Client)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            S3Config = s3Config.Value;
            S3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));   
        }

        public async Task<bool> PublishMessage(string messageKey, string message)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = S3Config.S3BucketName,
                    Key = messageKey,
                    InputStream = new MemoryStream(Encoding.UTF8.GetBytes(message)),
                    ContentType = "application/json",
                };

                var response = await S3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Logger.LogInformation($"Successfully uploaded message {messageKey} to S3 bucket: {S3Config.S3BucketName}.");
                    return true;
                }
                else
                {
                    Logger.LogInformation($"Could not upload {messageKey} to S3 bucket {S3Config.S3BucketName}.");
                    return false;
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

                return false;
            }
        }
    }
}

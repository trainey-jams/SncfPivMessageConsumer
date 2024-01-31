using Amazon;
using Amazon.S3;
using Amazon.SQS;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.AWS;

namespace PIV_POC_Client.AWS.ClientFactories.S3
{
    public interface IS3ClientFactory
    {
        IAmazonS3 GetS3Client();
    }

    public class S3ClientFactory : IS3ClientFactory
    {
        private readonly AWSCredentialConfig Credentials;
        private readonly S3Config S3Config;

        public S3ClientFactory(IOptions<AWSCredentialConfig> credentials, IOptions<S3Config> s3Config)
        {
            Credentials = credentials.Value;
            S3Config = s3Config.Value;
        }

        public IAmazonS3 GetS3Client()
        {
            return new AmazonS3Client(Credentials.AccessKey, Credentials.SecretKey, Credentials.SessionToken, RegionEndpoint.GetBySystemName(S3Config.Region));
        }
    }
}

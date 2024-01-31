using Amazon.S3;

namespace PIV_POC_Client.Interfaces
{
    public interface IS3ClientFactory
    {
        IAmazonS3 GetS3Client();
    }
}

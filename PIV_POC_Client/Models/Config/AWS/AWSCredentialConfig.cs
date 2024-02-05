namespace PIV_POC_Client.Models.Config.AWS
{
    public class AWSCredentialConfig
    {
        public string AccessKey { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public string SessionToken { get; set; } = string.Empty;

        public string TableName {  get; set; } = string.Empty;  

        public bool LocalMode { get; set; }
    }
}

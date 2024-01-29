namespace PIV_POC_Client.Models.Config.DAL
{
    public class SqlConnectionConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        public int ConnectionTimeout { get; set; }
    }
}

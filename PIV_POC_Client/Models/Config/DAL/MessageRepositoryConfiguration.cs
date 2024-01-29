namespace PIV_POC_Client.Models.Config.DAL
{
    public class MessageRepositoryConfiguration
    {
        public string InsertMessageProc { get; set; } = string.Empty;

        public string GetMessageProc { get; set; } = string.Empty;

        public string UpdateMessageProc { get; set;} = string.Empty;

        public string DeleteMessageProc { get; set; } = string.Empty;
    }
}

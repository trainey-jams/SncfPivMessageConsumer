namespace PIV_POC_Client.Interfaces
{
    public interface IMessageService
    {
        public Task ProcessPIVMessages(CancellationToken cancellationToken);
    }
}

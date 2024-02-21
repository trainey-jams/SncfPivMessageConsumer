namespace PIV_POC_Client.Interfaces
{
    public interface IMessageService
    {
        public Task ProcessMessages(CancellationToken cancellationToken);
    }
}

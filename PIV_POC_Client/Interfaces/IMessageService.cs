using Apache.NMS;

namespace PIV_POC_Client.Interfaces
{
    public interface IMessageService
    {
        public Task ProcessPIVMessages(IMessageConsumer consumer, CancellationToken cancellationToken);
    }
}

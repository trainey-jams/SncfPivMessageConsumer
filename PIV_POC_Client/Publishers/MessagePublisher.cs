using PIV_POC_Client.Interfaces;

namespace PIV_POC_Client.Publishers
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IS3Repository S3Repository;
        private readonly ISqsRepository SqsRepository;

        public MessagePublisher(IS3Repository s3Repository, ISqsRepository sqsRepository)
        {
            S3Repository = s3Repository ?? throw new ArgumentNullException(nameof(s3Repository));
            SqsRepository = sqsRepository ?? throw new ArgumentNullException(nameof(sqsRepository));
        }

        public async Task<bool> PublishMessage(string messageKey, string message)
        {
            var s3Result = await S3Repository.PublishMessage(messageKey, message);

            if (s3Result)
            {
                return await SqsRepository.PublishMessage(messageKey);
            }

            //maybe try and delete from s3 if sqs fails?

            return false;
        }
    }
}

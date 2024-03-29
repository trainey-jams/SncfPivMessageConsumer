﻿using Apache.NMS;
using SncfPivMessageConsumer.Interfaces;

namespace SncfPivMessageConsumer._OpenWire
{
    public interface IOpenWireSessionFactory
    {
        Task<ISession> GetSession(AcknowledgementMode acknowledgementMode);
    }

    public class OpenWireSessionFactory : IOpenWireSessionFactory
    {
        private readonly IOpenWireConnectionFactory ConnectionFactory;

        public OpenWireSessionFactory(IOpenWireConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<ISession> GetSession(AcknowledgementMode acknowledgementMode)
        {
            var connection = await ConnectionFactory.GetConnection();
            await connection.StartAsync();

            return await connection.CreateSessionAsync(acknowledgementMode);
        }
    }
}

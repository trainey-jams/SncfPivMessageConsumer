using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Serilog;
using Serilog.Debugging;
using SncfPivMessageConsumer.App;
using SncfPivMessageConsumer.AWS.Repos;
using SncfPivMessageConsumer.Channels;
using SncfPivMessageConsumer.Mappers;
using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.OpenWire;
using System.Text;
using System.Threading.Channels;
using Trainline.Extensions.Logging.Providers.Serilog.Extensions;

namespace SncfPivMessageConsumer_AcceptanceTests
{
    public class AcceptanceTests
    {
        ILoggerFactory LoggerFactory;

        public void Init()
        {
            var Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables().Build();

            var serviceProvider = new ServiceCollection()
                .AddTrainlineSerilogServices(Configuration)
                .BuildServiceProvider();

            var serilog = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            LoggerFactory = new LoggerFactory().AddSerilog(serilog);
        }

        [Fact]
        public async Task MessageServiceTest()
        {
            Init();
            var logger = LoggerFactory.CreateLogger<MessageService>();
            var channel = Channel.CreateBounded<ActiveMQMessageWrapper>(1);

            var service = new MessageService(logger, GetChannelProducer(), GetChannelConsumer(), channel);

            var token = new CancellationToken();

            await service.StartAsync(token);

            var x = 1;
        }

        private ChannelProducer GetChannelProducer()
        {
            var serviceContext = new ServiceContext(); 
            var consumerFactoryMock = Substitute.For<IOpenWireConsumerFactory>();
            var logger = LoggerFactory.CreateLogger<ChannelProducer>();
            var consumerMock = Substitute.For<IMessageConsumer>();
            
            ChannelProducer channelProducer = new ChannelProducer(serviceContext, logger, consumerFactoryMock);

            consumerFactoryMock.GetMessageConsumer().Returns(consumerMock);
            
            consumerMock.ReceiveAsync().Returns(GetMessage());

            return channelProducer;
        }

        private ChannelConsumer GetChannelConsumer()
        {
            var logger = LoggerFactory.CreateLogger<ChannelConsumer>();
            
            var pivMapper = new PivMapper();

            var snsRepo = Substitute.For<ISnsRepository>();

            ChannelConsumer channelConsumer = new ChannelConsumer(logger, pivMapper, snsRepo);

            snsRepo.PublishMessage("").Returns(true);

            return channelConsumer;
        }

        private ActiveMQMessage GetMessage()
        {
            return new ActiveMQMessage
            {
                Priority = 0,
                BrokerInTime = 1709895196,
                BrokerOutTime = 1709895198,
                Expiration = 1709895300,
                Content = Encoding.UTF8.GetBytes("PlaceholderForActualPivMessage")
            };
        }

    }
}
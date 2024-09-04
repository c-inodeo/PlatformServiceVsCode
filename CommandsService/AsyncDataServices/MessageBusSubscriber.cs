
using CommandsService.EventProcessing;
using RabbitMQ.Client;

namespace CommandsService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProceessor;

        public MessageBusSubscriber(
            IConfiguration configuration, 
            IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProceessor = eventProcessor;
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory() {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
        }
        //long running tasks, listen for events
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
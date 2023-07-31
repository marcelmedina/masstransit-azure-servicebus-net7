using MassTransit;
using Core.Events;

namespace Subscriber.Workers
{
    public class QueueProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<QueueProductCreatedConsumer> _logger;

        public QueueProductCreatedConsumer(ILogger<QueueProductCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var id = context.Message.Id;
            var name = context.Message.Name;
            var sku = context.Message.Sku;

            _logger.LogInformation($"Received - Product created: {id} - {name} - {sku}");

            return Task.CompletedTask;
        }
    }

    public class QueueProductCreatedConsumerDefinition : ConsumerDefinition<QueueProductCreatedConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<QueueProductCreatedConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}

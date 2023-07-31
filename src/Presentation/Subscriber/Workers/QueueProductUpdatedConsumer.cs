using MassTransit;
using Core.Events;

namespace Subscriber.Workers
{
    public class QueueProductUpdatedConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly ILogger<QueueProductUpdatedConsumer> _logger;

        public QueueProductUpdatedConsumer(ILogger<QueueProductUpdatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var id = context.Message.Id;
            var name = context.Message.Name;
            var sku = context.Message.Sku;

            _logger.LogInformation($"Received - Product updated: {id} - {name} - {sku}");

            return Task.CompletedTask;
        }
    }

    public class QueueProductUpdatedConsumerDefinition : ConsumerDefinition<QueueProductUpdatedConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<QueueProductUpdatedConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}

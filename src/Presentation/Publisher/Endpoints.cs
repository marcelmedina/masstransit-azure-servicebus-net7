using Core.Events;
using MassTransit;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Publisher
{
    public class Endpoints
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IMessageScheduler _scheduler;
        private readonly ILogger _logger;

        public Endpoints(
            ILoggerFactory loggerFactory, 
            IPublishEndpoint publisher,
            IMessageScheduler scheduler)
        {
            _publisher = publisher;
            _scheduler = scheduler;
            _logger = loggerFactory.CreateLogger<Endpoints>();
        }

        [Function(nameof(ProductCreated))]
        public async Task ProductCreated([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var productCreatedEvent = await req.ReadFromJsonAsync<ProductCreatedEvent>();

            if (productCreatedEvent != null)
            {
                await _publisher.Publish(productCreatedEvent, CancellationToken.None);

                _logger.LogInformation(
                    $"Product created: {productCreatedEvent.Id} - {productCreatedEvent.Name} - {productCreatedEvent.Sku}");
            }
        }

        [Function(nameof(ProductUpdated))]
        public async Task ProductUpdated([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var productUpdatedEvent = await req.ReadFromJsonAsync<ProductUpdatedEvent>();

            if (productUpdatedEvent != null)
            {
                await _publisher.Publish(productUpdatedEvent, CancellationToken.None);

                _logger.LogInformation(
                    $"Product updated: {productUpdatedEvent.Id} - {productUpdatedEvent.Name} - {productUpdatedEvent.Sku}");
            }
        }

        [Function(nameof(ProductCreatedScheduled))]
        public async Task ProductCreatedScheduled([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var productCreatedEvent = await req.ReadFromJsonAsync<ProductCreatedEvent>();

            if (productCreatedEvent != null)
            {
                await _scheduler.SchedulePublish(DateTime.UtcNow + TimeSpan.FromSeconds(20), productCreatedEvent);

                _logger.LogInformation(
                    $"Product created scheduled: {productCreatedEvent.Id} - {productCreatedEvent.Name} - {productCreatedEvent.Sku}");
            }
        }
    }
}

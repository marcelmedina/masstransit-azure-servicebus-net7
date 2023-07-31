using Core;
using Core.Events;
using MassTransit;
using Subscriber.Workers;

namespace Subscriber.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitSubscriber(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddServiceBusMessageScheduler();
                x.AddConsumer<QueueProductCreatedConsumer>(typeof(QueueProductCreatedConsumerDefinition));
                x.AddConsumer<QueueProductUpdatedConsumer>(typeof(QueueProductUpdatedConsumerDefinition));

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingAzureServiceBus((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("AzServiceBus"));
                    cfg.UseServiceBusMessageScheduler();

                    cfg.Message<ProductCreatedEvent>(m => m.SetEntityName(Constants.ProductCreatedTopic));
                    cfg.Message<ProductUpdatedEvent>(m => m.SetEntityName(Constants.ProductUpdatedTopic));

                    cfg.SubscriptionEndpoint<ProductCreatedEvent>(Constants.ProductCreatedSubscription, configurator =>
                    {
                        configurator.ConfigureConsumer<QueueProductCreatedConsumer>(ctx);
                    });

                    cfg.SubscriptionEndpoint<ProductUpdatedEvent>(Constants.ProductUpdatedSubscription, configurator =>
                    {
                        configurator.ConfigureConsumer<QueueProductUpdatedConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}

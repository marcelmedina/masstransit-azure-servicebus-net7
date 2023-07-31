using Core;
using Core.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddServiceBusMessageScheduler();
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingAzureServiceBus((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("AzServiceBus"));

                    cfg.UseServiceBusMessageScheduler();
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));

                    cfg.Message<ProductCreatedEvent>(m => m.SetEntityName(Constants.ProductCreatedTopic));
                    cfg.Message<ProductUpdatedEvent>(m => m.SetEntityName(Constants.ProductUpdatedTopic));
                });
            });

            return services;
        }
    }
}

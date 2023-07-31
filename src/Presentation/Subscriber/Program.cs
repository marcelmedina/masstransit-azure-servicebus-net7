using Subscriber.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitSubscriber(context.Configuration);
    })
    .Build();

host.Run();

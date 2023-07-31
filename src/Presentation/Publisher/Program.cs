using Microsoft.Extensions.Hosting;
using Publisher.Extensions;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitPublisher(context.Configuration);
    })
    .Build();

host.Run();

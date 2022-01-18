using SampleOutboxPattern.BackgroundWorker;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<OutboxOrderEventService>();
    })
    .Build();

await host.RunAsync();
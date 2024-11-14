var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransitExtension(builder.Configuration);
builder.Services.AddSerilog(nameof(CustomerCreatedListener));

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.ReceiveEndpoint("queue-teste", e =>
    {
        e.PrefetchCount = 10;
        e.UseMessageRetry(p => p.Interval(3, 100));
        e.Consumer<CustomerCreatedListener>();
    });
});
var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
await busControl.StartAsync(source.Token);

var host = builder.Build();
host.Run();

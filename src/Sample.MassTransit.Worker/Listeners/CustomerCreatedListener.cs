namespace Sample.MassTransit.Worker.Listeners;

public class CustomerCreatedListener : IConsumer<CustomerCreatedEvent>
{
    public Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        var message = context.Message.Message;

        Serilog.Log.Information("Received: {Nessage}", message);

        return Task.CompletedTask;
    }
}

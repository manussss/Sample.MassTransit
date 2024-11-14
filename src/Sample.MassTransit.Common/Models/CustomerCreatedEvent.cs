namespace Sample.MassTransit.Common.Models;

public class CustomerCreatedEvent
{
    public string Message { get; set; }

    public CustomerCreatedEvent(string message)
    {
        Message = message;
    }
}

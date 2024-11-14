using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sample.MassTransit.Common.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq((ctx, busConfigurator) =>
    {
        busConfigurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapPost("api/v1/message", async (
    [FromBody] CustomerCreatedEvent model,
    [FromServices] IPublishEndpoint publisher,
    [FromServices] ILogger<Program> logger) =>
{
    await publisher.Publish(model);
    logger.LogInformation($"Send {nameof(CustomerCreatedEvent)}");

    return TypedResults.Ok();
});

app.Run();

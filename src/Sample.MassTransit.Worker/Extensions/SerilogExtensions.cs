namespace Sample.MassTransit.Worker.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilog(this IServiceCollection services, string applicationName)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("MassTransit", LogEventLevel.Information)
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", $"{applicationName}")
            .WriteTo.Async(writeTo => writeTo.Console())
            .CreateLogger();
    }
}

using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SthlmTechEvents.BusinessLogic;

[assembly: FunctionsStartup(typeof(SthlmTechEvents.App.Startup))]
namespace SthlmTechEvents.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(
                lb => lb
                    .AddSerilog(
                        new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces)
                            .CreateLogger(),
                        true));
            
            builder.Services.AddTransient<IEventPublisherService, EventPublisherService>();
            builder.Services.AddTransient<IEventFetcher, MeetupEventFetcher>();
            builder.Services.AddTransient<IEventFetcher, EventbriteEventFetcher>();
            builder.Services.AddTransient<IEventPublisher, EventPublisher>();
        }
    }
}
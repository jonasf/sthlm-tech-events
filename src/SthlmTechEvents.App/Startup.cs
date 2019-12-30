using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SthlmTechEvents.BusinessLogic;

[assembly: FunctionsStartup(typeof(SthlmTechEvents.App.Startup))]
namespace SthlmTechEvents.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IEventPublisherService, EventPublisherService>();
            builder.Services.AddTransient<IEventFetcher, MeetupEventFetcher>();
            builder.Services.AddTransient<IEventFetcher, EventbriteEventFetcher>();
            builder.Services.AddTransient<IEventPublisher, EventPublisher>();
        }
    }
}
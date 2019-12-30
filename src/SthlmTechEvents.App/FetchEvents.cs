using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SthlmTechEvents.BusinessLogic;

namespace SthlmTechEvents.App
{
    public class FetchEvents
    {
        private readonly IEventPublisherService _eventPublisherService;

        public FetchEvents(IEventPublisherService eventPublisherService)
        {
            _eventPublisherService = eventPublisherService;
        }
        
        [FunctionName("FetchEvents")]
        public async Task RunAsync([TimerTrigger("0 5 * * * *"
#if DEBUG
                , RunOnStartup = true // TODO: Figure out why it won't run in debug mode         
#endif
                )] 
            TimerInfo myTimer, 
            ILogger log)
        {
            await _eventPublisherService.PublishEvents();
            log.LogInformation($"Fetch events function executed at: {DateTime.UtcNow}");
        }
    }
}
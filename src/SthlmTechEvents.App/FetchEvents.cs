using System;
using System.Diagnostics;
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
        private readonly ILogger<FetchEvents> _logger;

        public FetchEvents(IEventPublisherService eventPublisherService, ILogger<FetchEvents> logger)
        {
            _eventPublisherService = eventPublisherService;
            _logger = logger;
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
            var stopWatch = Stopwatch.StartNew();
            await _eventPublisherService.PublishEvents();
            stopWatch.Stop();
            _logger.LogInformation($"Fetch events function executed at: {DateTime.UtcNow:yyyy-MM-dd HH:mmss} in {stopWatch.ElapsedMilliseconds} ms.");
        }
    }
}
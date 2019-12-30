using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SthlmTechEvents.Data;

namespace SthlmTechEvents.BusinessLogic
{
    public class EventPublisherService : IEventPublisherService
    {
        private readonly ILogger<EventPublisherService> _logger;
        private readonly IEnumerable<IEventFetcher> _eventFetchers;
        private readonly IEventPublisher _eventPublisher;

        public EventPublisherService(ILogger<EventPublisherService> logger,
            IEnumerable<IEventFetcher> eventFetchers, 
            IEventPublisher eventPublisher)
        {
            _logger = logger;
            _eventFetchers = eventFetchers;
            _eventPublisher = eventPublisher;
        }
        
        public async Task PublishEvents()
        {
            var events = new List<Event>();

            foreach (var eventFetcher in _eventFetchers)
            {
                _logger.LogInformation($"Fetching events from {eventFetcher.Name}");
                var eventsFromProvider = await eventFetcher.GetEvents();
                _logger.LogInformation($"Found {eventsFromProvider.Count()} events from {eventFetcher.Name}");
                events.AddRange(eventsFromProvider);
            }

            await _eventPublisher.Publish(events);
        }
    }
}
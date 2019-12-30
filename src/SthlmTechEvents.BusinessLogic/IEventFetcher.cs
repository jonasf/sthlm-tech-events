using System.Collections.Generic;
using System.Threading.Tasks;
using SthlmTechEvents.Data;

namespace SthlmTechEvents.BusinessLogic
{
    public interface IEventFetcher
    {
        string Name { get; }
        Task<IEnumerable<Event>> GetEvents();
    }

    public class MeetupEventFetcher : IEventFetcher
    {
        public string Name => "Meetup";

        public Task<IEnumerable<Event>> GetEvents()
        {
            IEnumerable<Event> test = new List<Event> {new Event(), new Event()};
            return Task.FromResult(test);
        }
    }
    
    public class EventbriteEventFetcher : IEventFetcher
    {
        public string Name => "Eventbrite";
        
        public Task<IEnumerable<Event>> GetEvents()
        {
            IEnumerable<Event> test = new List<Event> {new Event()};
            return Task.FromResult(test);
        }
    }
}
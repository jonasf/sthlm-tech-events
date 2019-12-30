using System.Collections.Generic;
using System.Threading.Tasks;
using SthlmTechEvents.Data;

namespace SthlmTechEvents.BusinessLogic
{
    public interface IEventPublisher
    {
        Task Publish(IEnumerable<Event> events);
    }

    public class EventPublisher : IEventPublisher
    {
        public Task Publish(IEnumerable<Event> events)
        {
            return Task.CompletedTask;
        }
    }
}
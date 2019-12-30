using System.Threading.Tasks;

namespace SthlmTechEvents.BusinessLogic
{
    public interface IEventPublisherService
    {
        Task PublishEvents();
    }
}
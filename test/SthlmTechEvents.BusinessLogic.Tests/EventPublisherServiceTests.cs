using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SthlmTechEvents.Data;
using Xunit;

namespace SthlmTechEvents.BusinessLogic.Tests
{
    public class EventPublisherServiceTests
    {
        private readonly IEventFetcher _meetupFetcher;
        private readonly IEventFetcher _eventbriteFetcher;
        private readonly IEventPublisher _eventPublisher;

        private readonly EventPublisherService _sut;

        public EventPublisherServiceTests()
        {
            _meetupFetcher = Substitute.For<IEventFetcher>();
            _eventbriteFetcher = Substitute.For<IEventFetcher>();
            _eventPublisher = Substitute.For<IEventPublisher>();
            var fetchers = new List<IEventFetcher>
            {
                _meetupFetcher,
                _eventbriteFetcher
            };
            _sut = new EventPublisherService(Substitute.For<ILogger<EventPublisherService>>(), fetchers, _eventPublisher);
        }

        [Fact]
        public async Task Should_Publish_All_Events()
        {
            _meetupFetcher.GetEvents().Returns(new List<Event> {new Event(), new Event()});
            _eventbriteFetcher.GetEvents().Returns(new List<Event> {new Event()});
            
            await _sut.PublishEvents();

            await _meetupFetcher.Received(1).GetEvents();
            await _eventbriteFetcher.Received(1).GetEvents();
            await _eventPublisher.Received(1).Publish(Arg.Is<IEnumerable<Event>>(x => x.Count() == 3));
        }
    }
}
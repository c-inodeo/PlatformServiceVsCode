using AutoMapper;

namespace CommandsService.EventProcessing
{
    // take a message, and add a platform to dab
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            throw new NotImplementedException();
        }
    }
    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
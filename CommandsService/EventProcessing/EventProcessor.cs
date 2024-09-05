using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

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
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// To ensure what we got
        /// </summary>
        /// <param name="notificationMessage"></param>
        /// <returns></returns>
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("---> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("----> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("----> Could not determine that event type");
                    return EventType.Undetermined;
            }
        }
        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformExists(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine($"---> Platform added!");
                    }
                    else
                    {
                        Console.WriteLine($"---> Platform already exists");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"---> Could not add platform db: {ex.Message}");
                }

            }
        }
    }
    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
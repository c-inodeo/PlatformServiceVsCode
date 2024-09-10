using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILoggerFactory _loggerFactor;

        public PlatformDataClient(
            IConfiguration configuration, 
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _mapper = mapper;
            _loggerFactor = loggerFactory;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"--> Calling GRPC Services {_configuration["GrpcPlatform"]}");
            
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"],
             new GrpcChannelOptions {LoggerFactory = _loggerFactor});
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"-----> COULD NOT CALL GRPC SERVER {ex.Message}");
                return null;
            }
        }
    }
}
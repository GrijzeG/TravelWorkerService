using BingMapsAPIWorkerService.BingMaps;

namespace BingMapsAPIWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var routeRequests = FileIOHelper.ReadTwsRouteRequests();

                foreach (var routeRequest in routeRequests)
                {
                    var googleMapsResult = GoogleMapsLogic.GetRoute(routeRequest);
                    _logger.LogInformation(message: "{Description} Google Maps {Result}", routeRequest.Description, googleMapsResult.ToString());

                    var bingMapsResult = BingMapsLogic.GetRoute(routeRequest);
                    _logger.LogInformation(message: "{Description} Bing Maps {Result}", routeRequest.Description, bingMapsResult.ToString());

                    FileIOHelper.WriteResult(routeRequest.Description,googleMapsResult, bingMapsResult);
                }

                await Task.Delay(60 * 1000, stoppingToken); // 60 * 1 second
            }
        }
    }
}
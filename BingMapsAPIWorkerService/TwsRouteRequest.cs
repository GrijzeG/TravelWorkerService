using BingMapsRESTToolkit;

namespace BingMapsAPIWorkerService
{
    internal class TwsRouteRequest
    {
        public double FromLatitude { get; set; }
        public double FromLongitude { get; set; }
        public double ToLatitude { get; set; }
        public double ToLongitude { get; set; }
        public string? Description { get; set; }
    }
}

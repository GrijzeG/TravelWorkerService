using GoogleApi;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;

namespace BingMapsAPIWorkerService.BingMaps
{
    internal static class GoogleMapsLogic
    {
        private const string key = "google maps key";

        public static TwsRouteResult GetRoute(TwsRouteRequest request)
        {
            var directionsRequest = GetDirectionsRequest(request);
            var directionsResponse = GetDirectionsResponse(directionsRequest);

            return new TwsRouteResult()
            {
                TravelDistance = (double)directionsResponse.Routes.First().Legs.First().Distance.Value / 1000, // convert meters to km
                TravelDuration = directionsResponse.Routes.First().Legs.First().Duration.Value,
                TravelDurationTraffic = directionsResponse.Routes.First().Legs.First().DurationInTraffic.Value,
                FromAddress = directionsResponse.Routes.First().Legs.First().StartAddress,
                ToAddress = directionsResponse.Routes.First().Legs.First().EndAddress,
            };
        }

        static private DirectionsRequest GetDirectionsRequest(TwsRouteRequest request)
        {
            var from = new CoordinateEx(latitude: request.FromLatitude, longitude: request.FromLongitude);
            var to = new CoordinateEx(latitude: request.ToLatitude, longitude: request.ToLongitude);

            return new DirectionsRequest()
            {
                Key = key,
                Origin = new LocationEx(from),
                Destination = new LocationEx(to),
                DepartureTime = DateTime.Now,
                TrafficModel = GoogleApi.Entities.Maps.Common.Enums.TrafficModel.Best_Guess
            };
        }

        static private DirectionsResponse GetDirectionsResponse(DirectionsRequest request)
        {
            var directionsApi = new GoogleMaps.DirectionsApi();
            return directionsApi.Query(request);
        }
    }
}

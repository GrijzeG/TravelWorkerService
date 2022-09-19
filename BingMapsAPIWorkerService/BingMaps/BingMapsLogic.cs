using BingMapsRESTToolkit;

namespace BingMapsAPIWorkerService.BingMaps
{
    internal static class BingMapsLogic
    {
        private const string key = "bing maps key";

        public static TwsRouteResult GetRoute(TwsRouteRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var from = new Coordinate(latitude: request.FromLatitude, longitude: request.FromLongitude);
            var to = new Coordinate(latitude: request.ToLatitude, longitude: request.ToLongitude);

            var routeRequest = GetRouteRequest(from, to);
            var resources = GetResourcesFromRequest(routeRequest);
            
            var route = resources[0] as Route;

            return new TwsRouteResult()
            {
                TravelDistance = route?.TravelDistance,
                TravelDuration = route?.TravelDuration,
                TravelDurationTraffic = route?.TravelDurationTraffic,
                FromAddress = route?.RouteLegs?.First().RouteSubLegs?.First().StartWaypoint?.Description,
                ToAddress = route?.RouteLegs?.First().RouteSubLegs?.First().EndWaypoint?.Description,
            };
        }

        static private RouteRequest GetRouteRequest(Coordinate coordinate1, Coordinate coordinate2)
        {
            return new RouteRequest()
            {
                BingMapsKey = key,
                Waypoints = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint()
                    {
                         Coordinate = coordinate1
                    },
                    new SimpleWaypoint()
                    {
                         Coordinate = coordinate2
                    }
                },
                RouteOptions = new RouteOptions()
                {
                    Optimize = RouteOptimizationType.TimeWithTraffic
                }
            };
        }

        static private Resource[] GetResourcesFromRequest(BaseRestRequest rest_request)
        {
            var r = ServiceManager.GetResponseAsync(rest_request).GetAwaiter().GetResult();

            if (!(r != null && r.ResourceSets != null &&
                r.ResourceSets.Length > 0 &&
                r.ResourceSets[0].Resources != null &&
                r.ResourceSets[0].Resources.Length > 0))

                throw new Exception("No results found.");

            return r.ResourceSets[0].Resources;
        }
    }
}

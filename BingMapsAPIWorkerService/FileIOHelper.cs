using System.Globalization;
using CsvHelper;

namespace BingMapsAPIWorkerService
{
    internal class FileIOHelper
    {
        public static List<TwsRouteRequest> ReadTwsRouteRequests()
        {
            using var reader = new StreamReader(@"D:\TrafficWorkerService\Input\TwsRouteRequests.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TwsRouteRequest>().ToList();
        }

        public static void WriteTwsRouteRequests(List<TwsRouteRequest> requests)
        {
            using var writer = new StreamWriter(@"D:\TrafficWorkerService\Input\TwsRouteRequests.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(requests);
        }

        public static void WriteResult(string description, TwsRouteResult googleMapsResult, TwsRouteResult bingMapsResult)
        {
            if (googleMapsResult == null)
            {
                return;
            }

            if (bingMapsResult == null)
            {
                return;
            }

            string fileName = GetPathFileName(description);
            if (!File.Exists(fileName))
            {
                string header = $"Google Maps;{googleMapsResult.ToCsvHeader()};Bing Maps;{bingMapsResult.ToCsvHeader()}{Environment.NewLine}";
                File.WriteAllText(fileName, header);
            }

            string contents = $"Google Maps;{googleMapsResult.ToCsv()};Bing Maps;{bingMapsResult.ToCsv()}{Environment.NewLine}";
            File.AppendAllText(fileName, contents);
        }

        internal static string GetPathFileName(string description)
        {
            return $"{GetOutputPathName()}{GetValidFileName(description)}.csv";
        }

        internal static string GetOutputPathName()
        {
            return @"d:\TrafficWorkerService\Output\";
        }

        internal static string GetValidFileName(string description)
        {
            return Path.GetInvalidFileNameChars().Aggregate(description, (f, c) => f.Replace(c, '_'));
        }
    }
}

namespace BingMapsAPIWorkerService
{
    internal class TwsRouteResult
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public double? TravelDistance { get; set; }
        public double? TravelDuration { get; set; }
        public double? TravelDurationTraffic { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }  

        public string ToCsvHeader()
        {
            return $"{nameof(FromAddress)};{nameof(ToAddress)};{nameof(DateTime)};{nameof(TravelDistance)};{nameof(TravelDuration)};{nameof(TravelDurationTraffic)}";
        }

        public string ToCsv()
        {
            return $"{FromAddress};{ToAddress};{DateTime};{TravelDistance};{TravelDuration};{TravelDurationTraffic}";
        }

        public override string ToString()
        {
            return $"From {FromAddress} To {ToAddress} {Now()} Travel Distance: {TravelDistance} km - Travel Duration: {TravelDuration} seconds - Travel Duration Traffic:{TravelDurationTraffic} seconds";
        }

        static private string Now()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}

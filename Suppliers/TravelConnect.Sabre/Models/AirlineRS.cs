namespace TravelConnect.Sabre.Models
{
    public class AirlineRS : BaseRS
    {
        public Airlineinfo[] AirlineInfo { get; set; }
    }

    public class Airlineinfo
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string AlternativeBusinessName { get; set; }
    }
}
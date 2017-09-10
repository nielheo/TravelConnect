using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class AirportRS
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string IataCityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}, {1} ({2})", Name, CityName, Code);
            }
        }
    }
}
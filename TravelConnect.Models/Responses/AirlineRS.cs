using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class AirlineRS
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class AirportAutocompleteRS
    {
        public List<AirportRS> AirportsRS { get; set; }
    }
}

namespace TravelConnect.Sabre.Models
{
    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class BaseRS
    {
        public Link[] Links { get; set; }
    }
}
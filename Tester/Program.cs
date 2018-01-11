using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TravelConnect.Gta.Services.HotelService svc = new TravelConnect.Gta.Services.HotelService();
            var result = svc.SearchHotelPriceRequest();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}

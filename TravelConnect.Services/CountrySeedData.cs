using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models;

namespace TravelConnect.Services
{
    public static class CountrySeedData
    {
        private static async Task<T> ReadAsync<T>(string filename)
        {
            using (StreamReader myReader = File.OpenText(filename))
            {
                string source = await myReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<T>(source);
            }
        }

        public static void EnsureSeedData(this TCContext db)
        {
            var country = ReadAsync<List<Country>>("countries.json").Result;
            db.AddRange(country);
            db.SaveChanges();

            var cities = ReadAsync<List<City>>("cities.json").Result;
            db.AddRange(cities);
            db.SaveChanges();
        }
    }
}

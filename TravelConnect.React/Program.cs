using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using System.Xml;
using TravelConnect.CommonServices;

namespace TravelConnect_React
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogService _LogService = new LogService();
            
            _LogService.LogInfo("Entering application.");
            BuildWebHost(args).Run();
            _LogService.LogInfo("Exiting  application.");
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 100;
                })
                .Build();
    }
}
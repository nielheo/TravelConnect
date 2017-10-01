using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using System.Xml;

namespace TravelConnect_React
{
    public class Program
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {

            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            
            //var log = logRepository.GetLogger(typeof(Program).ToString());
            
            log.Info("Entering application.");
            BuildWebHost(args).Run();
            log.Info("Exiting  application.");
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
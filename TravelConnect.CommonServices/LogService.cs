using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace TravelConnect.CommonServices
{
    public interface ILogService
    {
        string LogInfo(string message, string threadId = "");
        string LogInfo(string title, object obj, string threadId = "");

        string LogException(Exception ex, string methodName, string threadId = "");
    }

    public class LogService : ILogService
    {
        private string _threadId;
        private static readonly log4net.ILog log = LogManager.GetLogger(typeof(LogService));

        private string ThreadId
        {
            get
            {
                if (string.IsNullOrEmpty(_threadId))
                {
                    _threadId = Guid.NewGuid().ToString().Replace("-", "");
                }

                return _threadId;
            }
        }

        public LogService()
        {
            _threadId = Guid.NewGuid().ToString().Replace("-", "");

            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public string LogException(Exception ex, string methodName, string threadId = "")
        {
            if (!string.IsNullOrEmpty(threadId))
                _threadId = threadId;

            log.Error(string.Format("[{0}] [{1}] Exception.Message: {2}",
                _threadId, methodName, ex.Message));
            log.Error(string.Format("[{0}] [{1}] Exception.Stack Trace: {2}",
                _threadId, methodName, ex.StackTrace));

            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, methodName, _threadId);
            }

            return _threadId;
        }

        public string LogInfo(string message, string threadId = "")
        {
            if (!string.IsNullOrEmpty(threadId))
                _threadId = threadId;
            //string thread = !string.IsNullOrEmpty(threadId) ? threadId : ThreadId;

            log.Info(string.Format("[{0}] {1}", _threadId, message));

            return _threadId;
        }

        public string LogInfo(string title, object obj, string threadId = "")
        {
            if (!string.IsNullOrEmpty(threadId))
                _threadId = threadId;
            //string thread = !string.IsNullOrEmpty(threadId) ? threadId : ThreadId;
            string json = JsonConvert.SerializeObject(obj,
                Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return LogInfo($"{title} - {json}");
        }
    }
}
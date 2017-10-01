using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace TravelConnect.CommonServices
{
    public interface ILogService
    {
        string LogInfo(string message, string threadId = "");
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
            string thread = !string.IsNullOrEmpty(threadId) ? threadId : ThreadId;
            
            log.Error(string.Format("[{0}] [{1}] Exception.Message: {2}", 
                thread, methodName, ex.Message));
            log.Error(string.Format("[{0}] [{1}] Exception.Stack Trace: {2}", 
                thread, methodName, ex.StackTrace));

            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, methodName, thread);
            }

            return thread;
        }

        public string LogInfo(string message, string threadId = "")
        {
            string thread = !string.IsNullOrEmpty(threadId) ? threadId : ThreadId;

            log.Info(string.Format("[{0}] {1}", thread, message));
            
            return thread;
        }
    }
}

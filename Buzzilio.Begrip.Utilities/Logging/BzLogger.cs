using Buzzilio.Begrip.Utilities.Logging.Interfaces;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace Buzzilio.Begrip.Utilities.Logging
{
    public class BzLogger : IBzLogger
    {
        /// <summary>
        /// 
        /// </summary>
        LoggingConfiguration NLogConfig = null;

        /// <summary>
        /// 
        /// </summary>
        public BzLogger(string directory)
        {
            CreateFolders(directory);
            NLogConfig = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            NLogConfig.AddTarget("file", fileTarget);

            fileTarget.FileName = "${specialfolder:folder=ApplicationData}/" + directory + "/error.log";
            fileTarget.Layout = "${longdate} [${logger}] ${message}";

            var ruleInfo = new LoggingRule("Info", LogLevel.Info, fileTarget);
            var ruleDebug = new LoggingRule("Debug", LogLevel.Info, fileTarget);
            var ruleException = new LoggingRule("Exception", LogLevel.Info, fileTarget);
            var ruleWarning = new LoggingRule("Warning", LogLevel.Info, fileTarget);

            NLogConfig.LoggingRules.Add(ruleInfo);
            NLogConfig.LoggingRules.Add(ruleDebug);
            NLogConfig.LoggingRules.Add(ruleException);
            NLogConfig.LoggingRules.Add(ruleWarning);

            LogManager.Configuration = NLogConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateFolders(string directory)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logPath = Path.Combine(appDataPath, directory);
            if (Directory.Exists(logPath)) { return; }
            Directory.CreateDirectory(logPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            LogManager.GetLogger("Info").Info(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            LogManager.GetLogger("Debug").Debug(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void LogException(string message, Exception e)
        {
            LogManager.GetLogger("Exception").Info(string.Format("{0}{1}", message, e.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            LogManager.GetLogger("Warning").Info(message);
        }
    }
}

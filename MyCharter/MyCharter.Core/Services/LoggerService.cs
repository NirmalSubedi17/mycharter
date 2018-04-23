using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using artm.MvxPlugins.Logger.Services;

namespace MyCharter.Core.Services
{
    public class LoggerService:ILoggerService
    {
        private static LoggerService _loggerService;
        private LoggerService()
        {
            
        }


        public static LoggerService Instance {
            get
            {
                if (_loggerService == null)
                    _loggerService = new LoggerService();
                return _loggerService;
            }
        
        }

        public void Log(string message, LoggerServiceSeverityLevel level = LoggerServiceSeverityLevel.Debug, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            //ToDo : Implement the actual logging;

            Debug.WriteLine(message);
        }
    }
}

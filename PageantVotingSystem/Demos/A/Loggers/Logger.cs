using System;

using Serilog;

namespace PageantVotingSystem.Source.Loggers
{
    public class Logger
    {
        public static bool IsLoggingEnabled = true;

        protected static Serilog.Core.Logger logger;

        protected static Serilog.Core.Logger SetupLogger(string logPath)
        {
            try
            {
                return new LoggerConfiguration().WriteTo.File(logPath).CreateLogger();
            }
            catch
            {
                throw new Exception($"'{logPath}' does not lead to an existing file");
            }
        }

        public static void LogInformation(string input, bool allowedToLog = true)
        {
            if (logger == null)
            {
                throw new Exception("'Logger' is not initialized");
            }
            if (!IsLoggingEnabled || !allowedToLog)
            {
                return;
            }

            logger.Information($"{input}");
        }

        public static void LogError(string input, bool allowedToLog = true)
        {
            if (logger == null)
            {
                throw new Exception("'Logger' is not initialized");
            }
            if (!IsLoggingEnabled || !allowedToLog)
            {
                return;
            }

            logger.Error($"{input}");
        }
    }
}

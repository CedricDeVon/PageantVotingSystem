
using System;

using Serilog;

namespace PageantVotingSystem.Sources.Loggers
{
    public class FileLogger
    {
        private static Serilog.Core.Logger logger;

        public static string LogOutputPath { get; private set; }

        public static void Setup(string logOutputPath)
        {
            try
            {
                logger = new LoggerConfiguration().WriteTo.File(logOutputPath).CreateLogger();
            }
            catch
            {
                throw new Exception($"'Logger' - File '{logOutputPath}' does not exist");
            }
            LogOutputPath = logOutputPath;
        }

        public static void LogInformationMessage(string input, bool allowedToLog = true)
        {
            if (!allowedToLog)
            {
                return;
            }

            logger.Information($"{input}");
        }

        public static void LogErrorMessage(string input, bool allowedToLog = true)
        {
            if (!allowedToLog)
            {
                return;
            }

            logger.Error($"{input}");
        }
    }
}


using System;

using Serilog;

namespace PageantVotingSystem.Sources.Loggers
{
    public class FileLogger : Logger
    {
        private Serilog.Core.Logger logger;

        public string LogOutputPath { get; private set; }

        public FileLogger(string logOutputPath) : base()
        {
            try
            {
                logger = new LoggerConfiguration().WriteTo.File(logOutputPath).CreateLogger();
                LogOutputPath = logOutputPath;
            }
            catch
            {
                throw new Exception($"'Logger' - File '{logOutputPath}' does not exist");
            }
        }

        public override void LogInformationMessage(string input)
        {
            if (!IsAllowedToLog)
            {
                return;
            }

            logger.Information($"{input}");
        }

        public override void LogErrorMessage(string input)
        {
            if (!IsAllowedToLog)
            {
                return;
            }

            logger.Error($"{input}");
        }
    }
}

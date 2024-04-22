using System;

using Serilog;

namespace PageantVotingSystem.Source.Utility
{
    public class Logger
    {
        public static bool isLoggingEnabled = ConfigurationSettings.TypeValue("IsLoggingEnabled") == "true";
        
        private static Serilog.Core.Logger logger = ValidateLogger();
        
        private Logger() {}

        private static Serilog.Core.Logger ValidateLogger()
        {
            string longOutputPath = ConfigurationSettings.Value("LogOutputPath");
            try
            {
                return new LoggerConfiguration()
                    .WriteTo.File(longOutputPath).CreateLogger();
            }
            catch
            {
                throw new Exception($"'{longOutputPath}' does not lead to an existing file");
            }
        }

        public static void LogInformation(string input, bool allowedToLog = true)
        {
            if (logger != null && isLoggingEnabled && allowedToLog)
            {
                logger.Information($"{ConfigurationSettings.Type}: {input}");
            }
        }

        public static void LogError(string input, bool allowedToLog = true)
        {
            if (logger != null && isLoggingEnabled && allowedToLog)
            {
                logger.Error($"{ConfigurationSettings.Type}: {input}");
            }            
        }
    }
}

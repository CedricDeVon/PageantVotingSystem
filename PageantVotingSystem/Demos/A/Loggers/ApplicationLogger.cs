
using PageantVotingSystem.Source.Caches;

namespace PageantVotingSystem.Source.Loggers
{
    public class ApplicationLogger : Logger
    {
        public static void Setup()
        {
            IsLoggingEnabled = ApplicationCache.Get<bool>("IsLoggingEnabled");
            logger = SetupLogger(ApplicationCache.Get<string>("LogOutputPath"));
        }
        
        public static void LogInformationToConsole(string input, bool allowedToLog = true)
        {
            Logger.LogInformation($"{ApplicationCache.Get<string>("TypeName")} : {input}", allowedToLog);
        }

        public static void LogInformationToFile(string input, bool allowedToLog = true)
        {
            Logger.LogInformation($"{ApplicationCache.Get<string>("TypeName")} : {input}", allowedToLog);
        }
        
        public static void LogErrorToConsole(string input, bool allowedToLog = true)
        {
            Logger.LogInformation($"{ApplicationCache.Get<string>("TypeName")} : {input}", allowedToLog);
        }

        public static void LogErrorToFile(string input, bool allowedToLog = true)
        {
            Logger.LogError($"{ApplicationCache.Get<string>("TypeName")} : {input}", allowedToLog);
        }
    }
}


using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Loggers
{
    public class ApplicationLogger
    {
        public static CompositeLogger CompositeLogger { get; private set; }
        
        public static void Setup(string fileLogOutputPath, bool isAllowedToLog)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationLogger");

            CompositeLogger = new CompositeLogger(fileLogOutputPath);
            CompositeLogger.IsAllowedToLog = isAllowedToLog;

            SetupRecorder.Add("ApplicationLogger");
        }

        public static void LogInformationMessage(string input)
        {
            CompositeLogger.LogInformationMessage(input);
        }

        public static void LogErrorMessage(string input)
        {
            CompositeLogger.LogErrorMessage(input);
        }
    }
}

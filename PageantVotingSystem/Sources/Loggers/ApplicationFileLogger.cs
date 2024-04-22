
using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Loggers
{
    public class ApplicationFileLogger : FileLogger
    {
        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationFileLogger");

            // ...

            SetupRecorder.Add("ApplicationFileLogger");
        }
    }
}

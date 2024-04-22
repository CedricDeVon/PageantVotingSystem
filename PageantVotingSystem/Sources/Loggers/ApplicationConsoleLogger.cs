
using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Loggers
{
    public class ApplicationConsoleLogger : ConsoleLogger
    {
        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationConsoleLogger");

            // ...

            SetupRecorder.Add("ApplicationConsoleLogger");
        }
    }
}

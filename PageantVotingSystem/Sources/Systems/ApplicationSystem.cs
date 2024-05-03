
using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Systems
{
    public class ApplicationSystem : System
    {
        public static string StringBuffer
        {
            get { return GetEnvironmentValue(stringBuffer); }

            private set
            {
                ThrowIfEnvironmentVariableDoesNotExist(value);
                stringBuffer = value;
            }
        }

        private static string stringBuffer;

        public static void Setup(string stringBuffer)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationSystem");
            ApplicationLogger.LogInformationMessage("'ApplicationSystem' setup began");
            
            StringBuffer = stringBuffer;

            SetupRecorder.Add("ApplicationSystem");
            ApplicationLogger.LogInformationMessage("'ApplicationSystem' setup complete");
        }
    }
}

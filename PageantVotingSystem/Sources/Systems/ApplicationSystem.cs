
using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Systems
{
    // blazing scrubs
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

            StringBuffer = stringBuffer;

            SetupRecorder.Add("ApplicationSystem");
        }
    }
}

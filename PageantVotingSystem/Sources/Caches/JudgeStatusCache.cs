
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Caches
{
    public class JudgeStatusCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }
            
            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("JudgeStatusCache");
            ApplicationLogger.LogInformationMessage("'JudgeStatusCache' setup began");

            Data.SetDataToPrivate("JudgeStatus", values);
            types = values;

            SetupRecorder.Add("JudgeStatusCache");
            ApplicationLogger.LogInformationMessage("'JudgeStatusCache' setup complete");
        }

        public static bool IsFound(object type)
        {
            return types.Contains(type);
        }
    }
}

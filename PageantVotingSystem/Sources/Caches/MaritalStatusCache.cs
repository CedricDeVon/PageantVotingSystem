
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Caches
{
    public class MaritalStatusCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("MaritalStatusCache");
            ApplicationLogger.LogInformationMessage("'MaritalStatusCache' setup began");

            Data.SetDataToPrivate("MaritalStatus", values);
            types = values;

            SetupRecorder.Add("MaritalStatusCache");
            ApplicationLogger.LogInformationMessage("'MaritalStatusCache' setup complete");
        }

        public static bool IsNotFound(object type)
        {
            return !IsFound(type);
        }

        public static bool IsFound(object type)
        {
            return types.Contains(type);
        }
    }
}

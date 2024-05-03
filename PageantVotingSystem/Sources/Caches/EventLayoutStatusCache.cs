
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Setups;


namespace PageantVotingSystem.Sources.Caches
{
    public class EventLayoutStatusCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("EventLayoutStatusCache");
            ApplicationLogger.LogInformationMessage("'EventLayoutStatusCache' setup began");

            Data.SetDataToPrivate("EventLayoutStatusCache", values);
            types = values;

            SetupRecorder.Add("EventLayoutStatusCache");
            ApplicationLogger.LogInformationMessage("'EventLayoutStatusCache' setup complete");
        }

        public static bool IsFound(object type)
        {
            return types.Contains(type);
        }

        public static bool IsNotFound(object type)
        {
            return !IsFound(type);
        }
    }
}

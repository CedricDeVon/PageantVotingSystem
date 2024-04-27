
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Caches
{
    public class ContestantStatusCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ContestantStatusCache");

            Data.SetPrivateData("ContestantStatus", values);
            types = values;

            SetupRecorder.Add("ContestantStatusCache");
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

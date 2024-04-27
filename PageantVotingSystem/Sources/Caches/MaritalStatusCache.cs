
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

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
            
            Data.SetPrivateData("MaritalStatus", values);
            types = values;

            SetupRecorder.Add("MaritalStatusCache");
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

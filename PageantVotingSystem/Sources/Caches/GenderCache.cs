
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Caches
{
    public class GenderCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("GenderCache");
            
            Data.SetPrivateData("Genders", values);
            types = values;

            SetupRecorder.Add("GenderCache");
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

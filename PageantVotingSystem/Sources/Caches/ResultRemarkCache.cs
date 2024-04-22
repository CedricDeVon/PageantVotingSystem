
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Caches
{
    public class ResultRemarkCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ResultRemarkCache");
            
            Data.SetPrivateData("ResultRemarks", values);
            types = values;

            SetupRecorder.Add("ResultRemarkCache");
        }

        public static bool IsFound(object type)
        {
            return types.Contains(type);
        }
    }
}

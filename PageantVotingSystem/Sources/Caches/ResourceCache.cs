
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Caches
{
    public class ResourceCache : Cache
    {
        public static HashSet<object> Names { get { return data.Keys.ToHashSet(); } }

        public static List<object> Paths { get { return data.Values.ToList(); } }

        private static Dictionary<object, object> data;

        public static void Setup(Dictionary<object, object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ResourceCache");

            Data.SetPrivateData("ResourceCache", values);
            data = values;

            SetupRecorder.Add("ResourceCache");
        }

        public static string GetPath(object name)
        {
            return (string) data[name];
        }

        public static bool IsFound(object name)
        {
            return data.ContainsKey(name);
        }
    }
}

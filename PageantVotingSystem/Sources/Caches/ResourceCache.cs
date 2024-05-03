
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Caches
{
    public class ResourceCache : Cache
    {
        public static HashSet<object> Names { get { return data.Keys.ToHashSet(); } private set { } }

        public static List<object> Paths { get { return data.Values.ToList(); } private set { } }

        private static Dictionary<object, object> data;

        public static void Setup(Dictionary<object, object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ResourceCache");
            ApplicationLogger.LogInformationMessage("'ResourceCache' setup began");

            Data.SetDataToPrivate("ResourceCache", values);
            data = values;

            SetupRecorder.Add("ResourceCache");
            ApplicationLogger.LogInformationMessage("'ResourceCache' setup complete");
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

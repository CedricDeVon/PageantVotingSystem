
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Caches
{
    public class UserRoleCache : Cache
    {
        public static HashSet<object> Types
        {
            get { return types.ToHashSet(); }

            private set { }
        }

        private static HashSet<object> types;

        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("UserRoleCache");
            ApplicationLogger.LogInformationMessage("'UserRoleCache' setup began");

            Data.SetDataToPrivate("UserRoles", values);
            types = values;

            SetupRecorder.Add("UserRoleCache");
            ApplicationLogger.LogInformationMessage("'UserRoleCache' setup complete");
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

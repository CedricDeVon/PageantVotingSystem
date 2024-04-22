using System;
using System.Configuration;

using PageantVotingSystem.Source.Caches;

namespace PageantVotingSystem.Source.Configurations
{
    public class ApplicationConfiguration : Configuration
    {
        public static string TypeNameValue(string key)
        {
            string typeName = ApplicationCache.Get<string>("TypeName");
            string value = ConfigurationManager.AppSettings[$"{typeName}.{key}"];
            if (value == null)
            {
                throw new Exception($"'{typeName}.{key}' configuration key does not exist");
            }
            return value;
        }

    }
}

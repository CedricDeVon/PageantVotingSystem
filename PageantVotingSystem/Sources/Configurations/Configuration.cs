
using System;
using System.Configuration;

namespace PageantVotingSystem.Sources.Configurations
{
    public class Configuration
    {
        // All values retrieved from 'App.config' are of type 'string'.
        public static string GetAppConfigValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            ThrowIfAppConfigKeyIsNull(key, value);

            return value;
        }

        protected static void ThrowIfAppConfigKeyIsNull(string key, string value)
        {
            if (value == null)
            {
                throw new Exception($"'Configuration' - App.config key '{key}' does not exist");
            }
        }
    }
}


using System;
using System.Configuration;

namespace PageantVotingSystem.Sources.Configurations
{
    public class Configuration
    {
        public static string GetApplicationConfigurationValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            ThrowIfApplicationConfigurationKeyIsNull(key, value);
            return value;
        }

        protected static void ThrowIfApplicationConfigurationKeyIsNull(string key, string value)
        {
            if (value == null)
            {
                throw new Exception($"'Configuration' - App.config key '{key}' does not exist");
            }
        }
    }
}

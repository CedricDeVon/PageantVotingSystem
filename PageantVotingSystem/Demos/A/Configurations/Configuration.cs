using System;

using System.Configuration;

namespace PageantVotingSystem.Source.Configurations
{
    public class Configuration
    {
        // All values retrieved from 'App.config' are of type 'string'.
        public static string Value(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                throw new Exception($"'{key}' configuration key does not exist");
            }
            return value;
        }

        public static string EnvironmentValue(string key)
        {
            string keyValue = Value(key);
            string environmentValue = Environment.GetEnvironmentVariable(keyValue, EnvironmentVariableTarget.User);
            if (environmentValue == null)
            {
                throw new Exception($"'{keyValue}' environment variable does not exist");
            }
            return environmentValue;
        }
    }
}
   
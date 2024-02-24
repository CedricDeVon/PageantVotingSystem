using System;
using System.Collections.Generic;

using System.Configuration;

namespace PageantVotingSystem.Source.Utility
{
    public class ConfigurationSettings
    {
        public static string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value == null || !validTypes.Contains(value))
                {
                    throw new Exception($"'{value}' must either be: 'Development', 'Testing' or 'Production'");   
                }
                type = value;
            }
        }

        private readonly static List<string> validTypes = new List<string>() { "Development", "Testing", "Production" };

        private static string type = ValidateType();
        
        private static string ValidateType()
        {
            Type = Value("Type");
            return type;
        }

        private ConfigurationSettings() { }

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

        public static string TypeValue(string key)
        {
            string value = ConfigurationManager.AppSettings[$"{Type}.{key}"];
            if (value == null)
            {
                throw new Exception($"'{type}.{key}' configuration key does not exist");
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
   
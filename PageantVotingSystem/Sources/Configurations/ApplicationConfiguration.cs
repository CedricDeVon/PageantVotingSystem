
using System;
using System.Configuration;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Configurations
{
    public class ApplicationConfiguration : Configuration
    {
        public static string CurrentTypeName
        {
            get { return currentTypeName; }

            private set
            {
                ThrowIfInvalidTypeName(value);

                currentTypeName = value;
            }
        }

        public static string DefaultUserProfileImagePath
        {
            get { return defaultUserProfileImagePath; }

            private set
            {
                defaultUserProfileImagePath = value; //
            }
        }

        private static string currentTypeName;

        private static string defaultUserProfileImagePath;

        private readonly static HashSet<string> validTypeNames = new HashSet<string>();

        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationConfiguration");

            SetupTypeNames();
            CurrentTypeName = GetAppConfigValue("CurrentTypeName");
            DefaultUserProfileImagePath = GetAppConfigValue("DefaultUserProfileImagePath");

            SetupRecorder.Add("ApplicationConfiguration");
        }

        public static string GetTypedAppConfigValue(string key)
        {
            key = $"{CurrentTypeName}.{key}";
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"'ApplicationConfiguration' - App.config key '{key}' does not exist");
            }
            return value;
        }

        private static void SetupTypeNames()
        {
            try
            {
                foreach (string validTypeName in GetAppConfigValue("ValidTypeNames").Split(new char[] { ',' }))
                {
                    validTypeNames.Add(validTypeName);
                }
            }
            catch
            {
                throw new Exception("'ApplicationConfiguration' - App.config attribute 'TypeNames' parsing failed");
            }
        }

        private static void ThrowIfInvalidTypeName(string key)
        {
            if (!validTypeNames.Contains(key))
            {
                throw new Exception($"'ApplicationConfiguration' - Type name '{key}' is invalid");
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Source.Databases;
using PageantVotingSystem.Source.Configurations;
using PageantVotingSystem.Source.Utilities;

namespace PageantVotingSystem.Source.Caches
{
    public class ApplicationCache : Cache
    {
        private static bool isConfigurationSettingsLoaded = false;

        private static bool isDatabaseDataLoaded = false;

        public static void SetupConfigurationSettings()
        {
            if (isConfigurationSettingsLoaded)
            {
                throw new Exception("'ApplicationCache' configuration settings are already loaded");
            }

            Set("TypeNames", ApplicationConfiguration.Value("TypeNames").Split(new char[] { ',' }).ToHashSet());
            Set("TypeName", ApplicationConfiguration.Value("TypeName"));
            Set("LogOutputPath", ApplicationConfiguration.Value("LogOutputPath"));
            Set("StringBuffer", ApplicationConfiguration.Value("StringBuffer"));
            Set("IsLoggingEnabled", Convert.ToBoolean(ApplicationConfiguration.TypeNameValue("IsLoggingEnabled")));
            
            Set("EmailMaximumLength", Convert.ToInt32(ApplicationConfiguration.Value("EmailMaximumLength")));
            Set("FullNameMaximumLength", Convert.ToInt32(ApplicationConfiguration.Value("FullNameMaximumLength")));
            Set("PasswordMaximumLength", Convert.ToInt32(ApplicationConfiguration.Value("PasswordMaximumLength")));
            Set("PasswordMinimumLength", Convert.ToInt32(ApplicationConfiguration.Value("PasswordMinimumLength")));
            
            Set("DatabaseName", ApplicationConfiguration.Value("DatabaseName"));
            Set("DatabaseHostName", ApplicationConfiguration.TypeNameValue("DatabaseHostName"));
            Set("DatabasePortNumber", ApplicationConfiguration.TypeNameValue("DatabasePortNumber"));
            Set("DatabaseUserName", ApplicationConfiguration.TypeNameValue("DatabaseUserName"));
            Set("DatabaseSetupFilePath", ApplicationConfiguration.TypeNameValue("DatabaseSetupFilePath"));

            isConfigurationSettingsLoaded = true;
        }

        public static void SetupDatabaseData()
        {
            if (isDatabaseDataLoaded)
            {
                throw new Exception("'ApplicationCache' database data are already loaded");
            }

            UpdateCurrentUser();
            SetupUserRoleTypes();
            SetupResultRemarkTypes();
            SetupCurrentStatusTypes();
            SetupScoringSystemTypes();

            isDatabaseDataLoaded = true;
        }

        public static void UpdateCurrentUser(string fullName = "", string roleType = "", string email = "")
        {
            Set("UserFullName", fullName);
            Set("UserRoleType", roleType);
            Set("UserEmail", email);
        }

        public static bool isUserRoleTypeFound(string roleType)
        {
            return Get("UserRoleTypes").Get<HashSet<string>>().Contains(roleType);
        }

        private static void SetupUserRoleTypes()
        {
            HashSet<string> types = new HashSet<string>();
            Set("UserRoleTypes", types);
            Result databaseResult = ApplicationDatabase.ReadUserRoleTypes();
            if (!databaseResult.IsSuccessful)
            {
                throw new Exception(databaseResult.ExceptionMessage);
            }
            foreach (Dictionary<string, object> entity in databaseResult.Data)
            {
                types.Add((string) entity["type"]);
            }
        }

        private static void SetupResultRemarkTypes()
        {
            HashSet<string> types = new HashSet<string>();
            Set("ResultRemarkTypes", types);
            Result databaseResult = ApplicationDatabase.ReadResultRemarkTypes();
            if (!databaseResult.IsSuccessful)
            {
                throw new Exception(databaseResult.ExceptionMessage);
            }
            foreach (Dictionary<string, object> entity in databaseResult.Data)
            {
                types.Add((string)entity["type"]);
            }
        }

        private static void SetupCurrentStatusTypes()
        {
            HashSet<string> types = new HashSet<string>();
            Set("CurrentStatusTypes", types);
            Result databaseResult = ApplicationDatabase.ReadCurrentStatusTypes();
            if (!databaseResult.IsSuccessful)
            {
                throw new Exception(databaseResult.ExceptionMessage);
            }
            foreach (Dictionary<string, object> entity in databaseResult.Data)
            {
                types.Add((string)entity["type"]);
            }
        }

        private static void SetupScoringSystemTypes()
        {
            HashSet<string> types = new HashSet<string>();
            Set("ScoringSystemTypes", types);
            Result databaseResult = ApplicationDatabase.ReadScoringSystemTypes();
            if (!databaseResult.IsSuccessful)
            {
                throw new Exception(databaseResult.ExceptionMessage);
            }
            foreach (Dictionary<string, object> entity in databaseResult.Data)
            {
                types.Add((string)entity["type"]);
            }
        }
    }
}

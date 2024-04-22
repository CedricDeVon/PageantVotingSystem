using System;

using PageantVotingSystem.Source.Configurations;
using PageantVotingSystem.Source.Utilities;
using PageantVotingSystem.Source.Caches;

namespace PageantVotingSystem.Source.Databases
{
    public class ApplicationDatabase : Database
    {
        public static string DatabaseName
        {
            get { return currentSettings.DatabaseName; }

            set
            {
                string oldDatabaseName = currentSettings.DatabaseName;
                string hostName = ApplicationCache.Get<string>("DatabaseHostName");
                string portNumber = ApplicationCache.Get<string>("DatabasePortNumber");
                string userName = ApplicationCache.Get<string>("DatabaseUserName");
                string databaseName = $"{value}_{ApplicationCache.Get<string>("TypeName")}";
                string stringBuffer = ApplicationConfiguration.EnvironmentValue("StringBuffer");
                currentSettings = new DatabaseSettings(hostName, portNumber, userName, databaseName, stringBuffer);
                usingStatement = (!string.IsNullOrEmpty(currentSettings.DatabaseName)) ? $"USE {currentSettings.DatabaseName}; " : "";
                // Logger.LogInformation($"Database name updated from '{oldDatabaseName}' to '{currentSettings.DatabaseName}'");
            }
        }

        private static bool isLoaded = false;

        public static void Setup()
        {
            if (isLoaded)
            {
                throw new Exception("'ApplicationDatabase' is already loaded");
            }

            string hostName = ApplicationCache.Get<string>("DatabaseHostName");
            string portNumber = ApplicationCache.Get<string>("DatabasePortNumber");
            string userName = ApplicationCache.Get<string>("DatabaseUserName");
            string databaseName = $"{ApplicationCache.Get<string>("DatabaseName")}_{ApplicationCache.Get<string>("TypeName")}";
            string stringBuffer = ApplicationConfiguration.EnvironmentValue("StringBuffer");
            currentSettings = new DatabaseSettings(hostName, portNumber, userName, databaseName, stringBuffer);
            DatabaseName = ApplicationCache.Get<string>("DatabaseName");
            Result result = ExecuteFile(ApplicationCache.Get<string>("DatabaseSetupFilePath"));
            if (!result.IsSuccessful)
            {
                throw new Exception(result.ExceptionMessage);
            }

            isLoaded = true;
        }

        public static Result ReadOneUserRoleType(string roleType)
        {
            return ExecuteStatement($"SELECT type FROM user_role WHERE type = '{roleType}'");
        }
        
        public static Result ReadUserRoleTypes()
        {
            return ExecuteStatement($"SELECT type FROM user_role");
        }

        public static Result ReadResultRemarkTypes()
        {
            return ExecuteStatement($"SELECT type FROM result_remark");
        }

        public static Result ReadCurrentStatusTypes()
        {
            return ExecuteStatement($"SELECT type FROM current_status");
        }
        public static Result ReadScoringSystemTypes()
        {
            return ExecuteStatement($"SELECT type FROM scoring_system");
        }

        public static Result ReadOneUserEmail(string email)
        {
            return ExecuteStatement($"SELECT email FROM user WHERE email = '{email}'");
        }

        public static Result CreateUser(string email, string fullName, string password, string roleType)
        {
            return ExecuteStatement($"INSERT INTO user (email, full_name, password, user_role_type) VALUES ('{email}', '{fullName}', '{password}', '{roleType}')");
        }

        public static Result ReadOneUser(string email)
        {
            return ExecuteStatement($"SELECT email, full_name, password, user_role_type FROM user WHERE email = '{email}'");
        }
    }
}

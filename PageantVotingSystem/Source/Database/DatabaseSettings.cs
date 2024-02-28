using System;

using PageantVotingSystem.Source.Utility;

namespace PageantVotingSystem.Source.Database
{
    public class DatabaseSettings
    {
        public string DatabaseName { get; private set; }

        public string TableName { get; private set; }

        public string DatabaseTableName { get; private set; }

        public string HostName { get; private set; }

        public string PortNumber { get; private set; }

        public string UserName { get; private set; }

        public string ConnectionString { get; private set; }

        public string BaseConnectionString { get; private set; }

        public DatabaseSettings()
        {
            SetDeafultAttributes();
        }

        public DatabaseSettings(string databaseName)
        {
            DatabaseName = GenerateConfiguredDatabaseName(databaseName);
            SetDeafultAttributes();
        }

        public DatabaseSettings(DatabaseSettings settings)
        {
            if (settings == null)
            {
                throw new Exception();
            }

            TableName = settings.TableName;
            DatabaseName = settings.DatabaseName;
            DatabaseTableName = settings.DatabaseTableName;
            HostName = settings.HostName;
            PortNumber = settings.PortNumber;
            UserName = settings.UserName;
            ConnectionString = settings.ConnectionString;
        }

        private void SetDeafultAttributes()
        {
            HostName = ConfigurationSettings.TypeValue("DatabaseHostName");
            PortNumber = ConfigurationSettings.TypeValue("DatabasePortNumber");
            UserName = ConfigurationSettings.TypeValue("DatabaseUserName");
            GenerateConnectionString();
        }

        private string GenerateConfiguredDatabaseName(string name)
        {
            return (!string.IsNullOrEmpty(name)) ? $"{name}_{ConfigurationSettings.Value("Type").ToLower()}" : "";
        }

        private string GenerateDatabaseTableName()
        {
            return $"{DatabaseName}.{TableName}";
        }

        private void GenerateConnectionString()
        {
            Logger.LogInformation("Connection string generated");
            BaseConnectionString = $"server={HostName};";
            BaseConnectionString += $"port={PortNumber};";
            BaseConnectionString += $"uid={UserName};";
            BaseConnectionString += (!string.IsNullOrEmpty(DatabaseName)) ? $"database={DatabaseName};" : "";
            ConnectionString = BaseConnectionString;
            ConnectionString += $"pwd={ConfigurationSettings.EnvironmentValue("StringBuffer")};";
        }
    }
}

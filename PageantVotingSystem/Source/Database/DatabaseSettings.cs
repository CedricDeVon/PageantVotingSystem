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
            ConnectionString = GenerateConnectionString();
        }

        private string GenerateConfiguredDatabaseName(string name)
        {
            return (!string.IsNullOrEmpty(name)) ? $"{name}_{ConfigurationSettings.Value("Type").ToLower()}" : "";
        }

        private string GenerateDatabaseTableName()
        {
            return $"{DatabaseName}.{TableName}";
        }

        private string GenerateConnectionString()
        {
            string connectionString = $"server={HostName};";
            connectionString += $"port={PortNumber};";
            connectionString += $"uid={UserName};";
            connectionString += $"port={PortNumber};";
            connectionString += (!string.IsNullOrEmpty(DatabaseName)) ? $"database={DatabaseName};" : "";
            connectionString += $"pwd={ConfigurationSettings.EnvironmentValue("StringBuffer")};";
            return connectionString;
        }
    }
}

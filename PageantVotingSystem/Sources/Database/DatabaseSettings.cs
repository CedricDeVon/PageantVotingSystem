
using System;

namespace PageantVotingSystem.Sources.Databases
{
    public class DatabaseSettings
    {
        public string DatabaseName { get; private set; }

        public string TableName { get; private set; }

        public string DatabaseTableName { get; private set; }

        public string HostName { get; private set; }

        public string PortNumber { get; private set; }

        public string UserName { get; private set; }

        public string SetupFilePath { get; private set; }

        public string ConnectionString { get; private set; }

        public string BaseConnectionString { get; private set; }

        public DatabaseSettings(
            string hostName,
            string portNumber,
            string userName,
            string stringBuffer,
            string databaseName = "",
            string setupFilePath = "")
        {
            DatabaseName = databaseName;
            SetDeafultAttributes(
                hostName,
                portNumber,
                userName,
                stringBuffer);
            SetupFilePath = setupFilePath;
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

        private void SetDeafultAttributes(
            string hostName,
            string portNumber,
            string userName,
            string stringBuffer)
        {
            HostName = hostName;
            PortNumber = portNumber;
            UserName = userName;
            GenerateConnectionString(stringBuffer);
        }

        private void GenerateConnectionString(string stringBuffer)
        {
            BaseConnectionString = $"server={HostName};";
            BaseConnectionString += $"port={PortNumber};";
            BaseConnectionString += $"uid={UserName};";
            BaseConnectionString += (!string.IsNullOrEmpty(DatabaseName)) ?
                $"database={DatabaseName};" : "";
            ConnectionString = BaseConnectionString;
            ConnectionString += $"pwd={stringBuffer};";
        }
    }
}

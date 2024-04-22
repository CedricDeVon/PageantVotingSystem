
using System;
using System.IO;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using PageantVotingSystem.Sources.Results;

namespace PageantVotingSystem.Sources.Databases
{
    public class Database
    {
        public static DatabaseSettings CurrentSettings { get; protected set; }

        public static Result ExecuteStatement(string mySqlStatement)
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(CurrentSettings.ConnectionString);
                MySqlCommand mySqlCommand = GenerateMySqlCommand(mySqlConnection, mySqlStatement);
                mySqlConnection.Open();
                List<Dictionary<object, object>> data = ReadData(mySqlCommand.ExecuteReader());
                mySqlConnection.Close();
                return new ResultSuccess(data);
            }
            catch (Exception exception)
            {
                return new ResultFailed(exception);
            }
        }

        public static Result ExecuteFile(string filePath)
        {
            try
            {
                return ExecuteStatement(File.ReadAllText(filePath));
            }
            catch (Exception exception)
            {
                return new ResultFailed(exception);
            }
        }

        public static void Connect(
            string hostName,
            string portNumber,
            string userName,
            string stringBuffer,
            string databaseName = "")
        {
            Connect(
                new DatabaseSettings(
                    hostName,
                    portNumber,
                    userName,
                    stringBuffer,
                    databaseName));
        }

        public static void Connect(DatabaseSettings newSettings)
        {
            ThrowIfDatabaseSettingsIsNull(newSettings);
            TestConnection(newSettings);

            CurrentSettings = newSettings;
        }

        protected static void TestConnection(DatabaseSettings settings)
        {
            ThrowIfDatabaseSettingsIsNull(settings);

            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(settings.ConnectionString);
                MySqlCommand mySqlCommand = GenerateMySqlCommand(mySqlConnection);
                mySqlConnection.Open();
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private static MySqlCommand GenerateMySqlCommand(
            MySqlConnection mySqlConnection,
            string mySqlStatement = "")
        {
            ThrowIfMySqlConnectionIsNull(mySqlConnection);

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandText = mySqlStatement;
            return mySqlCommand;
        }

        private static List<Dictionary<object, object>> ReadData(MySqlDataReader reader)
        {
            ThrowIfMySqlReaderIsNull(reader);

            List<Dictionary<object, object>> data = new List<Dictionary<object, object>>();
            while (reader.Read())
            {
                Dictionary<object, object> row = new Dictionary<object, object>();
                for (int index = 0; index < reader.FieldCount; index++)
                {
                    row[reader.GetName(index)] = reader.GetValue(index);
                }
                data.Add(row);
            }
            return data;
        }

        private static void ThrowIfDatabaseSettingsIsNull(DatabaseSettings databaseSettings)
        {
            if (databaseSettings == null)
            {
                throw new Exception("'Database' - 'databaseSettings' must not be null");
            }
        }

        private static void ThrowIfMySqlReaderIsNull(MySqlDataReader reader)
        {
            if (reader == null)
            {
                throw new Exception("'Database' - 'reader' must not be null");
            }
        }

        private static void ThrowIfMySqlConnectionIsNull(MySqlConnection mySqlConnection)
        {
            if (mySqlConnection == null)
            {
                throw new Exception("'Database' - 'mySqlConnection' must not be null");
            }
        }
    }
}

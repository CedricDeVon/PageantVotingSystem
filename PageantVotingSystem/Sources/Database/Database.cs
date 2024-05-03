
using System;
using System.IO;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Databases
{
    public class Database
    {
        public static DatabaseSettings CurrentSettings { get; protected set; }

        public static Result ExecuteStatement(string mySqlStatement)
        {
            try
            {
                ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' connecting to '{CurrentSettings.SimplifiedConnectionString}'");
                MySqlConnection mySqlConnection =
                    new MySqlConnection(CurrentSettings.CompleteConnectionString);
                MySqlCommand mySqlCommand = GenerateMySqlCommand(mySqlConnection, mySqlStatement);
                mySqlConnection.Open();
                ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' connected at '{CurrentSettings.SimplifiedConnectionString}'");
                List<Dictionary<object, object>> data = ReadData(mySqlCommand.ExecuteReader());
                mySqlConnection.Close();
                ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' closed from '{CurrentSettings.SimplifiedConnectionString}'");
                return new ResultSuccess(data);
            }
            catch (Exception exception)
            {
                ApplicationLogger.LogErrorMessage(exception.Message);
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
                ApplicationLogger.LogErrorMessage(exception.Message);
                return new ResultFailed(exception);
            }
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
                ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' testing connection at '{CurrentSettings.SimplifiedConnectionString}'");
                MySqlConnection mySqlConnection = new MySqlConnection(settings.CompleteConnectionString);
                MySqlCommand mySqlCommand = GenerateMySqlCommand(mySqlConnection);
                mySqlConnection.Open();
                mySqlConnection.Close();
                ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' connection tested from '{CurrentSettings.SimplifiedConnectionString}'");
            }
            catch (Exception exception)
            {
                ApplicationLogger.LogErrorMessage(exception.Message);
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
            ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' reading data at '{CurrentSettings.SimplifiedConnectionString}'");

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

            ApplicationLogger.LogInformationMessage($"'ApplicationDatabase' data read at '{CurrentSettings.SimplifiedConnectionString}'");
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

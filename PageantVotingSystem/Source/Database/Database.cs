using System;
using System.IO;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace PageantVotingSystem.Source.Database
{
    public class Database
    {
        public static DatabaseSettings CurrentSettings
        {
            get { return currentSettings; }

            private set { }
        }

        public static string Name
        {
            get { return currentSettings.DatabaseName; }

            set
            {
                currentSettings = new DatabaseSettings(value);
                usingStatement = (!string.IsNullOrEmpty(currentSettings.DatabaseName)) ? $"USE {currentSettings.DatabaseName}; " : "";
            }
        }

        private static string usingStatement;

        private static DatabaseSettings currentSettings = new DatabaseSettings();

        public static DatabaseOutput ExecuteStatement(string mySqlStatement)
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(currentSettings.ConnectionString);
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = $"{usingStatement}{mySqlStatement}";
                mySqlConnection.Open();
                List<Dictionary<string, object>> data = ReadData(mySqlCommand.ExecuteReader());
                mySqlConnection.Close();
                return DatabaseOutput.Success(data);
            }
            catch (Exception exception)
            {
                return DatabaseOutput.Failure(exception);
            }
        }

        public static DatabaseOutput ExecuteFile(string filePath)
        {
            try
            {
                return ExecuteStatement(File.ReadAllText(filePath));
            }
            catch (Exception exception)
            {
                return DatabaseOutput.Failure(exception);
            }
            
        }

        private static List<Dictionary<string, object>> ReadData(MySqlDataReader reader)
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (int index = 0; index < reader.FieldCount; index++)
                {
                    row[reader.GetName(index)] = reader.GetValue(index);
                }
                data.Add(row);
            }
            return data;
        }
    }
}

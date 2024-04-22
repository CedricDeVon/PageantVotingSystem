using System;
using System.IO;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using PageantVotingSystem.Source.Utilities;

namespace PageantVotingSystem.Source.Databases
{
    public class Database
    {
        public static DatabaseSettings CurrentSettings
        {
            get { return currentSettings; }

            private set { }
        }

        protected static string usingStatement;

        protected static DatabaseSettings currentSettings;

        public static Result ExecuteStatement(string mySqlStatement)
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(currentSettings.ConnectionString);
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = $"{usingStatement}{mySqlStatement}";
                mySqlConnection.Open();
                // Logger.LogInformation($"MySqlConnection opened at '{currentSettings.BaseConnectionString}'");
                // Logger.LogInformation($"MySql statement began execution");
                List<Dictionary<string, object>> data = ReadData(mySqlCommand.ExecuteReader());
                // Logger.LogInformation($"MySql statement completed execution");
                mySqlConnection.Close();
                // Logger.LogInformation($"MySqlConnection closed from '{currentSettings.BaseConnectionString}'");
                return Result.Success(data);
            }
            catch (Exception exception)
            {
                // Logger.LogError(exception.Message);
                return Result.Failure(exception);
            }
        }

        public static Result ExecuteFile(string filePath)
        {
            try
            {
                // Logger.LogInformation($"MySql file began execution at '{filePath}'");
                Result databaseOutput = ExecuteStatement(File.ReadAllText(filePath));
                // Logger.LogInformation($"MySql file completed execution from '{filePath}'");
                return databaseOutput;
            }
            catch (Exception exception)
            {
                // Logger.LogError(exception.Message);
                return Result.Failure(exception);
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

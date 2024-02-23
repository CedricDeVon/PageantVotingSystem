using PageantVotingSystem;
using System;

public class DataAccessLayer
{
    private DataBaseHelper dbHelper;

    public DataAccessLayer(string connectionString)
    {
        dbHelper = new DataBaseHelper(connectionString);
    }

    public void InsertData(string data)
    {
        using (var connection = dbHelper.GetConnection())
        {
            try
            {
                connection.Open();
                // Perform INSERT operation
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // Other CRUD operations methods
}

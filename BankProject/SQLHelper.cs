using System;
using System.Data.SQLite;

namespace BankProject
{

    public class SQLHelper
    {
        public static string connectionString = "Data Source=Database.db;Version=3;";

        public static int DoQuery(string query)
        {
            int rowsAffected = 0;
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR | While preforming the query" + e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return rowsAffected;
        }
    }
}

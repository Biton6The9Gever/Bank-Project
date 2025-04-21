using System;
using System.Data;
using System.Data.SQLite;

namespace BankProject
{

    public class SQLHelper
    {
        public static string connectionString = "Data Source=Database.db;Version=3;";

        public static DataTable SelectData(string query)
        {
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        // Open connection to the database
                        connection.Open();
                        command.CommandTimeout = 0;

                        // Execute the query and fill the DataTable
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR | While selecing data table" + e.Message);
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            return dt;
        }

        public static int DoQuery(string query)
        {
            // This method is used to execute a query on the database file
            int rowsAffected = 0;

            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        // Open connection to the database
                        connection.Open();
                        // Execute the query and Update the number of rows affected
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR | While preforming the query" + e.Message);
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            return rowsAffected;
        }
    }
}

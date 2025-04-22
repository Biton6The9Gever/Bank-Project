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
                        Console.WriteLine("ERROR | While selecting data table from query" + e.Message);
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

        public static object SelectScalar(string query)
        {
            object result = null;
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        // Open connection to the database
                        connection.Open();
                        // Execute the query and get the result
                        result = command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR | While selecting saclar from query" + e.Message);
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            return result;
        }
        public static bool SelectScalarToBool(string query)
        {
            // Select the answer as object
            object obj = SelectScalar(query);
            if(obj != null)
            {
                return Convert.ToBoolean(obj);
            }  
            else
            {
                Console.WriteLine("ERROR | While preforming the query");
                return false;
            }
        }

        public static void PrintDataTable(DataTable dt)
        {
            // Check if the DataTable is empty
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }

            // Calculate max width of each column
            int[] columnWidths = new int[dt.Columns.Count];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                columnWidths[j] = dt.Columns[j].ColumnName.Length;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int len = dt.Rows[i][j].ToString().Length;
                    if (len > columnWidths[j])
                        columnWidths[j] = len;
                }
            }

            // Print the DataTable in a formatted way

            // Print a separator line
            void PrintSeparator()
            {
                Console.Write("+");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(new string('-', columnWidths[j] + 2));
                    Console.Write("+");
                }
                Console.WriteLine();
            }

            // Print header
            PrintSeparator();
            Console.Write("|");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string name = dt.Columns[j].ColumnName;
                Console.Write(" " + name.PadRight(columnWidths[j]) + " |");
            }
            Console.WriteLine();
            PrintSeparator();

            // Print rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.Write("|");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string value = dt.Rows[i][j].ToString();
                    Console.Write(" " + value.PadRight(columnWidths[j]) + " |");
                }
                Console.WriteLine();
            }
            PrintSeparator();
        }


    }
}

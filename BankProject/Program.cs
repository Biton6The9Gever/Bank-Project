using System;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace BankProject
{
    internal class Program
    {
        public static void CreateTable(string dbFileName)
        {
            // Freandly message to the user
            Console.WriteLine("[==========================================]");
            Console.WriteLine($"You about to create .db file named \"{dbFileName}\"");
            Console.WriteLine("are you sure you wanna procced ? [Y/n]");
            Console.WriteLine("[==========================================]");
            // Check if the user is sure to create the file
            if (Console.ReadLine().ToLower() == "y")
            {
                dbFileName += ".db";
                // Check if the file already exist
                if (File.Exists(dbFileName))
                {
                    Console.WriteLine("ERROR | Table already Exist");
                }
                else
                {
                    // Try to create the file
                    try
                    {
                        SQLiteConnection.CreateFile(dbFileName);
                        Console.WriteLine("Database file created!");
                    }
                    // If an error occurs, catch it and display a message
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR | creating database file: " + e.Message);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string query = @"SELECT * FROM Users";

            DataTable dt = SQLHelper.SelectData(query);

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

            // Helper to print separator line
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

            // Print data rows
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

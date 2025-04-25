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
            //ILoginHandler loginHandler = new LoginHandler();
            //Console.WriteLine(loginHandler.Login()); ;
            string query = @"INSERT INTO Users (Username, Password, FirstName, LastName, AccountNumber)
VALUES ('biton1', 'biton1', 'biton1', 'biton1', 1);
";
            SQLHelper.DoQuery(query);
            DataTable dt =SQLHelper.SelectData("SELECT * FROM Users");
            SQLHelper.PrintDataTable(dt);

        }
    }
}

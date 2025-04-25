using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{
    public interface ILoginHandler
    {
        bool Login();
        bool CheckLogin(string username, string password);
        string GetPassword();
    }
    public interface IRegiseterHandler
    {
        bool Register();
        string GetPassword();
    }
    public class LoginHandler : ILoginHandler, IRegiseterHandler
    {
        public LoginHandler()
        {
            
        }
        //TODO hash the password
        public bool Login()
        {
            Console.WriteLine("[==========================================]");
            Console.WriteLine($"        Login - Page");
            Console.WriteLine("[==========================================]");
            Console.Write(" Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("\n Enter your password: ");
            string password = GetPassword();

            return CheckLogin(username, password);
        }
        public bool CheckLogin(string username,string password)
        {
            // Check if the username and password are correct
            string query = $"SELECT COUNT(*) FROM Users WHERE username = '{username}' AND password = '{password}'";
            bool answer = SQLHelper.SelectScalarToBool(query);
            if(answer)
            {
                return true;
            }
            return false;
        }

        public string GetPassword()
        {
            // This method is used to get the password from the user and hide it
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return password;
        }

        public bool Register()
        {
            Console.WriteLine("[==========================================]");
            Console.WriteLine($"        Register - Page");
            Console.WriteLine("[==========================================]");
            Console.Write(" Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("\n Enter your password: ");
            string password = GetPassword();
            Console.Write("\n Confirm your password: ");
            string confirmPassword = GetPassword();
            Console.Write("\n Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("\n Enter your last name: ");
            string lastName = Console.ReadLine();
            //TODO hash the password
            if(confirmPassword != password)
            {
                Console.WriteLine("ERROR | Passwords do not match");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            string query = $"SELECT COUNT(*) FROM Users WHERE username = '{username}'";
            if(SQLHelper.SelectScalarToBool(query))
            {
                Console.WriteLine("ERROR | Username already exists");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            query = $"INSERT INTO Users (Username, Password, FirstName, LastName) VALUES ('{username}', '{password}', '{firstName}', '{lastName}')";
            int rowsAffected = SQLHelper.DoQuery(query);
            if (rowsAffected > 0)
            {
                Console.WriteLine("Logging in !");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("ERROR | Something went wrong while logging you in please contact support!");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

        }
    }
}

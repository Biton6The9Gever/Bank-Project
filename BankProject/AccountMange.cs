using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{
    public class AccountMange
    {
        public void CreateAccount()
        {
            if (SessionManger.CurrentUser != (null, 0))
            {
                Console.WriteLine("[==========================================]");
                Console.WriteLine($"        Create Account - Page");
                Console.WriteLine("[==========================================]");
                Console.Write(" Enter your account name: ");
                string accountName = Console.ReadLine();
                Console.Write(" \n [1] Business \n [2] Personal \n [3] Saving \n Enter your account type: ");
                string accountType = Console.ReadLine();
                if(accountType == "1")
                    accountType = "Business";
                else if (accountType == "2")
                    accountType = "Personal";
                else if (accountType == "3")
                    accountType = "Saving";
                else
                {
                    Console.WriteLine("\n ERROR | Invalid account type");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                //TODO hash the password
                string query = $"INSERT INTO Accounts (UserID, Balance, AccountType) VALUES ('{SessionManger.CurrentUser.Item2}', {0}, '{accountType}')";
                int rowsAffected = SQLHelper.DoQuery(query);
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Account created successfully");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("ERROR | You need to login first");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

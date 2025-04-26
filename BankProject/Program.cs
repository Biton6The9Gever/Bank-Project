using System;

namespace BankProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ILoginHandler loginHandler = new LoginHandler();
            Console.WriteLine(loginHandler.Login());

            AccountMange accountMange = new AccountMange();
            accountMange.CreateAccount();

        }
    }
}

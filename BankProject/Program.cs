using System;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace BankProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRegiseterHandler regiseterHandler = new LoginHandler();
            Console.WriteLine(regiseterHandler.Register());

            ILoginHandler loginHandler = new LoginHandler();
            Console.WriteLine(loginHandler.Login()); 


        }
    }
}

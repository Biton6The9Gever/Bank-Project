using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{
    // This class is used to manage the session of the user
    public class SessionManger
    {
        // if no user is logged in value is (null ,0)
        public static (string,int) CurrentUser { get; set; } 
    }
}

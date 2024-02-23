using PageantVotingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem_Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool logsuccess = false;
            string username, name, password, role;

            Console.WriteLine("Username");
            username = Console.ReadLine();
            Console.WriteLine("Password");
            password = Console.ReadLine();
            if (username != null && password != null)
            {
                LogIn login = new LogIn(username, password);
                logsuccess = login.AuthenticateUser(username, password);
            }
            if (logsuccess == true)
            {
                AddPage addPage = new AddPage();
            }

            Console.ReadKey();
        }
    }
}

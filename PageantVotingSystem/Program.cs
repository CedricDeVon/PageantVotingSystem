using System;

namespace PageantVotingSystem
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //username: Ysmael PW: Jajalla

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

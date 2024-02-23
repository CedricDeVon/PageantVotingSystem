using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Logging;
using Serilog;

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

            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            var log = new LoggerConfiguration()
                .WriteTo.File("../../Logs/log.txt")
                .CreateLogger();
            log.Information("Information");
            log.Error("Error");
            log.Verbose("Verbose");
            log.Warning("Warning");


            //dynamic array = JsonConvert.DeserializeObject(json);
            //foreach (var item in array)
            //{
            //    Console.WriteLine("{0} {1}", item.temp, item.vcc);
            //}

            //MessageBox.Show(System.Environment.GetEnvironmentVariable("FIND_ME", EnvironmentVariableTarget.User));
            //System.Environment.SetEnvironmentVariable("FIND_ME", "GOOD", EnvironmentVariableTarget.User);
            //MessageBox.Show(System.Environment.GetEnvironmentVariable("FIND_ME", EnvironmentVariableTarget.User));

            // System.Environment.SetEnvironmentVariable(variable, value [, Target]);
        }
    }
}

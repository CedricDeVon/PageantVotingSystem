using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PageantVotingSystem.Source.Utility;
using System.Text;
using System.Net.Sockets;
using PageantVotingSystem.Source.Database;

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
            //Database.Name = "demo";
            //Console.WriteLine(Database.Name);
            //Console.WriteLine(Database.ExecuteStatement("DELETE FROM user").IsOk);
            //Console.WriteLine(Database.ExecuteStatement("INSERT INTO user (full_name, age) VALUES ('X', 1)").IsOk);
            //Console.WriteLine(Database.ExecuteStatement("INSERT INTO user (full_name, age) VALUES ('Y', 2)").IsOk);
            //Console.WriteLine(Database.ExecuteStatement("INSERT INTO user (full_name, age) VALUES ('Z', 3)").IsOk);
            //Console.WriteLine(Database.ExecuteStatement("UPDATE user SET full_name = 'eh' WHERE age = 1").IsOk);
            //foreach (Dictionary<string, object> a in Database.ExecuteStatement("SELECT * FROM user ORDER BY id DESC LIMIT 100").Data)
            //{
            //    foreach (KeyValuePair<string, object> b in a)
            //    {
            //        Console.WriteLine($"{b.Key}, {b.Value}");
            //    }
            //}
            //foreach (Dictionary<string, object> a in Database.ExecuteFile("../../DatabaseSetup/demo.sql").Data)
            //{
            //    foreach (KeyValuePair<string, object> b in a)
            //    {
            //        Console.WriteLine($"{b.Key}, {b.Value}");
            //    }
            //}

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}

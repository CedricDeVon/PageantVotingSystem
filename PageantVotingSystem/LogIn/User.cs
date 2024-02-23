using System;
using System.IO;

namespace PageantVotingSystem
{
    public class User
    {
        protected string UserName { get; set; }
        protected string FullName { get; set; }
        protected string Password { get; set; }
        protected string UserRole { get; set; }

        protected string filepath = "User.txt";
        public User()
        {

        }
        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public void CreateUser(string UserName, string FullName, string Password, string UserRole)
        {

            this.UserName = UserName;
            this.FullName = FullName;
            this.Password = Password;
            this.UserRole = UserRole;
            WriteToFile(filepath, UserName, Password);
        }
        public void WriteToFile(string filepath, string UserName, string Password)
        {
            try
            {
                string data = "username, password\n" +
                               UserName + "," + Password;
                File.WriteAllText(filepath, data);
                Console.WriteLine("User data has been written to the file successfully.");
            }
            catch
            {
                Console.WriteLine($"An error occurred while writing to the file");
            }
        }
    }
}

using System;
using System.IO;
namespace PageantVotingSystem
{
    public class LogIn : User
    {
        public LogIn(string UserName, string Password) : base(UserName, Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }


        public bool AuthenticateUser(string UserName, string Password)
        {

            // Read lines from the file
            string[] lines = File.ReadAllLines(filepath);

            // Start loop from the second line
            for (int i = 1; i < lines.Length; i++)
            {
                // Split the line into username and password
                string[] parts = lines[i].Split(',');

                // Check if username and password match
                if (UserName == parts[0] && Password == parts[1])
                {
                    Console.WriteLine("Login successful!");
                    return true; // Exit the method since authentication is successful
                }
            }

            // If no matching user is found
            Console.WriteLine("Invalid username or password. Please try again.");
            return false;
        }
    }
}

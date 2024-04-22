
using System;

namespace PageantVotingSystem.Sources.Loggers
{
    public class ConsoleLogger
    {
        public static void LogInformationMessage(string input, bool allowedToLog = true)
        {
            LogMessage("Information", input, allowedToLog);
        }

        public static void LogErrorMessage(string input, bool allowedToLog = true)
        {
            LogMessage("Error", input, allowedToLog);
        }

        public static void LogMessage(string type, string input, bool allowedToLog = true)
        {
            if (!allowedToLog)
            {
                return;
            }

            Console.WriteLine(GenerateLogMessageFormat(type, input));
        }

        private static string GenerateLogMessageFormat(string type, string input)
        {
            return $"[{type}] [{DateTime.Now}] - {input}";
        }
    }
}

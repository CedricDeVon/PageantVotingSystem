
using System;

namespace PageantVotingSystem.Sources.Loggers
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger() : base() {}

        public override void LogInformationMessage(string input)
        {
            LogMessage("Information", input);
        }

        public override void LogErrorMessage(string input)
        {
            LogMessage("Error", input);
        }

        public void LogMessage(string type, string input)
        {
            if (!IsAllowedToLog)
            {
                return;
            }

            Console.WriteLine(GenerateLogMessageFormat(type, input));
        }

        private string GenerateLogMessageFormat(string type, string input)
        {
            return $"[{type}] [{DateTime.Now}] - {input}";
        }
    }
}

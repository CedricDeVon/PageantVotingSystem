using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Loggers
{
    public class CompositeLogger : Logger
    {
        public FileLogger FileLogger { get; private set; }

        public ConsoleLogger ConsoleLogger { get; private set; }

        public CompositeLogger(string fileLogOutputPath) : base()
        {
            FileLogger = new FileLogger(fileLogOutputPath);
            ConsoleLogger = new ConsoleLogger();
        }

        public override void LogInformationMessage(string input)
        {
            if (!IsAllowedToLog)
            {
                return;
            }

            FileLogger.LogInformationMessage(input);
            ConsoleLogger.LogInformationMessage(input);
        }

        public override void LogErrorMessage(string input)
        {
            if (!IsAllowedToLog)
            {
                return;
            }

            FileLogger.LogErrorMessage(input);
            ConsoleLogger.LogErrorMessage(input);
        }
    }
}

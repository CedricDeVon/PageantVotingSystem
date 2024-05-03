using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Loggers
{
    public abstract class Logger
    {
        public bool IsAllowedToLog { get; set; }

        public Logger()
        {
            IsAllowedToLog = true;
        }

        public abstract void LogInformationMessage(string input);

        public abstract void LogErrorMessage(string input);
    }
}

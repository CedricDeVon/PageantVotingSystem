
using System;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.FormNavigators;

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
            ApplicationSetup.Execute();
            ApplicationFormNavigator.BeginDisplay("StartingMenu");

            /* 
             * 
             */
        }
    }
}


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

            /* Targets:
             * / Implement MySQL Judge Test Data
             * / Implement MySQL Administer Test Data
             * 
             * / JudgeCriteriumResultItem
             * / JudgeCriteriumResultItemLayout
             * 
             * / Implementing MessageDialogBox
             * / Implementing ConfirmationDialogBox
             * 
             * / Assign Layout Validation Constraints
             * / Features Collection
             * / Implement Age Calculation
             * 
             * / Assign Age Calculation
             * 
             * / Define ApplicationValidation For Edit Event
             * / Rename ApplicationValidation Messages
             * / Assign ApplicationValidation For Edit Event
             * 
             * - ManagerDashboard Buttons (EditEvent and AdministerEvent)
             * - Clear Buttons
             * 
             * - JudgeDashboard
             * - AdministerEvent
             * - AdministerEventContestant
             * - AdministerEventJudges
             * - AdministerEventLayout
             * - AdministerVotingSession
             * 
             * - Assign ConfirmationDialogBox
             * - Assign MessageDialogBox
             * - Assign Logging
             */
        }
    }
}

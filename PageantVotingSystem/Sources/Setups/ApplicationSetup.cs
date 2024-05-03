
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Forms;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Systems;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Configurations;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Setups
{
    public class ApplicationSetup
    {
        public static void Execute()
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationSetup");

            ApplicationConfiguration.Setup();
            ApplicationLogger.Setup(
                ApplicationConfiguration.GetApplicationConfigurationValue("LogOutputPath"),
                Convert.ToBoolean(ApplicationConfiguration.GetTypedApplicationConfigurationValue("IsLoggingEnabled")));
            ApplicationLogger.LogInformationMessage("'ApplicationSetup' setup began");
            ApplicationLogger.LogInformationMessage("'ApplicationConfiguration' setup complete");
            ApplicationLogger.LogInformationMessage("'ApplicationLogger' setup complete");

            ApplicationSystem.Setup(ApplicationConfiguration.GetApplicationConfigurationValue("StringBuffer"));
            ApplicationFormStyle.Setup();

            ApplicationDatabase.Setup
            (
                new DatabaseSettings
                (
                    ApplicationConfiguration.GetTypedApplicationConfigurationValue("DatabaseHostName"),
                    ApplicationConfiguration.GetTypedApplicationConfigurationValue("DatabasePortNumber"),
                    ApplicationConfiguration.GetTypedApplicationConfigurationValue("DatabaseUserName"),
                    ApplicationSystem.GetEnvironmentValue("StringBuffer"),
                    $"{ApplicationConfiguration.GetTypedApplicationConfigurationValue("DatabaseName")}_{ApplicationConfiguration.CurrentTypeName}",
                    ApplicationConfiguration.GetTypedApplicationConfigurationValue("DatabaseSetupFilePath")
                )
            );

            GenderCache.Setup(ApplicationDatabase.ReadAllRawGenders());
            UserRoleCache.Setup(ApplicationDatabase.ReadAllRawUserRoles());
            RoundStatusCache.Setup(ApplicationDatabase.ReadAllRawRoundStatus());
            JudgeStatusCache.Setup(ApplicationDatabase.ReadAllRawJudgeStatus());
            MaritalStatusCache.Setup(ApplicationDatabase.ReadAllRawMaritalStatus());
            ScoringSystemCache.Setup(ApplicationDatabase.ReadAllRawScoringSystems());
            ApplicationResourceLoader.Setup(ApplicationDatabase.ReadAllRawResources());
            ContestantStatusCache.Setup(ApplicationDatabase.ReadAllRawContestantStatus());
            EventLayoutStatusCache.Setup(ApplicationDatabase.ReadAllRawEventLayoutStatus());
            RoundContestantStatusCache.Setup(ApplicationDatabase.ReadAllRawRoundContestantStatus());

            TypeConstraintCache.Setup();
            UserProfileCache.Setup();
            AdministerEventCache.Setup();
            EditEventCache.Setup();

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            ApplicationFormNavigator.Setup
            (
                new List<Form>()
                {
                    new StartingMenu(),
                    new About(),
                    new LogIn(),
                    new SignUp(),
                    new UserProfile(),
                    new EditUserProfile(),
                    new ManagerDashboard(),
                    new JudgeContestantDashboard(),
                    new JudgeDashboard(),
                    new AdministerEventQuery(),
                    new AdministerEvent(),
                    new AdministerEventContestants(),
                    new AdministerEventContestantReview(),
                    new AdministerVotingSession(),
                    new EventResults(),
                    new EventJudges(),
                    new EventContestants(),
                    new EventContestantProfile(),
                    new EventLayout(),
                    new EventProfile(),
                    new EventSegmentProfile(),
                    new EventRoundProfile(),
                    new EventCriteriumProfile(),
                    new EventCriteriumResult(),
                    new EventRoundResult(),
                    new EventSegmentResult(),
                    new EditEvent(),
                    new EditEventProfile(),
                    new EditEventSegmentStructure(),
                    new EditEventRoundStructure(),
                    new EditEventCriteriumStructure(),
                    new EditEventJudges(),
                    new EditEventContestants()
                },
                new Background(),
                new InformationNotFound()
            );

            SetupRecorder.Add("ApplicationSetup");
            ApplicationLogger.LogInformationMessage("'ApplicationSetup' setup complete");
        }
    }
}

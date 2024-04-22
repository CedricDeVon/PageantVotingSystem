
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Forms;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Systems;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;
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
            ApplicationSystem.Setup
            (
                ApplicationConfiguration.GetAppConfigValue("StringBuffer")
            );
            ApplicationFileLogger.Setup
            (
                ApplicationConfiguration.GetAppConfigValue("LogOutputPath")
            );
            ApplicationDatabase.Setup
            (
                new DatabaseSettings
                (
                    ApplicationConfiguration.GetTypedAppConfigValue("DatabaseHostName"),
                    ApplicationConfiguration.GetTypedAppConfigValue("DatabasePortNumber"),
                    ApplicationConfiguration.GetTypedAppConfigValue("DatabaseUserName"),
                    ApplicationSystem.GetEnvironmentValue("StringBuffer"),
                    $"{ApplicationConfiguration.GetTypedAppConfigValue("DatabaseName")}_{ApplicationConfiguration.CurrentTypeName}",
                    ApplicationConfiguration.GetTypedAppConfigValue("DatabaseSetupFilePath")
                )
            );
            ApplicationFormStyle.Setup();



            ContestantStatusCache.Setup(ApplicationDatabase.ReadContestantStatus());
            GenderCache.Setup(ApplicationDatabase.ReadGenders());
            JudgeStatusCache.Setup(ApplicationDatabase.ReadJudgeStatus());
            MaritalStatusCache.Setup(ApplicationDatabase.ReadMaritalStatus());
            ResultRemarkCache.Setup(ApplicationDatabase.ReadResultRemarks());
            RoundStatusCache.Setup(ApplicationDatabase.ReadRoundStatus());
            ScoringSystemCache.Setup(ApplicationDatabase.ReadScoringSystems());
            UserRoleCache.Setup(ApplicationDatabase.ReadUserRoles());
            ApplicationResourceLoader.Setup(ApplicationDatabase.ReadResources());
            TypeConstraintCache.Setup(ApplicationDatabase.ReadTypeConstraints());
            UserProfileCache.Setup(new UserEntity());
            EditEventContestantCache.Setup(new List<ContestantEntity>());
            EditEventJudgesCache.Setup(new List<string>());
            EditEventCache.Setup
            (
                new EventEntity(),
                new GenericOrderedList<string>(),
                new GenericOrderedList<ContestantEntity>()
            );



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
                    new ManagerDashboard(),
                    new UserProfile(),
                    new EditUserProfile(),
                    new EditEvent(),
                    new EditEventProfile(),
                    new EditEventSegmentStructure(),
                    new EditEventRoundStructure(),
                    new EditEventCriteriumStructure(),
                    new EditEventJudges(),
                    new EditEventContestants(),
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
                    new AdministerEvent(),
                    new AdministerEventContestants(),
                    new AdministerEventJudges(),
                    new AdministerEventLayout(),
                    new AdministerVotingSession(),
                    new JudgeDashboard()
                },
                new Background(),
                new InformationNotFound()
            );
            SetupRecorder.Add("ApplicationSetup");
        }
    }
}

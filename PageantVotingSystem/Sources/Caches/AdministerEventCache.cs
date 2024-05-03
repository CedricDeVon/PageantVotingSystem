
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Caches
{
    public class AdministerEventCache : Cache
    {
        public static EventLayoutSequenceEntity EventLayoutSequence { get; set; }

        public static List<RoundEntity> RoundEntities { get; set; }

        public static List<JudgeUserEntity> JudgeEntities { get; set; }
        
        public static List<ContestantEntity> ContestantEntities { get; set; }

        public static int QualifiedContestantCount
        {
            get
            {
                int count = 0;
                foreach (ContestantEntity contestantEntity in ContestantEntities)
                {
                    if (contestantEntity.ContestantStatusType == "Qualified")
                    {
                        count += 1;
                    }
                }
                return count;
            }

            private set { }
        }

        public static ContestantEntity SelectedContestant { get; set; }

        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("AdministerEventCache");
            ApplicationLogger.LogInformationMessage("'AdministerEventCache' setup began");

            EventLayoutSequence = new EventLayoutSequenceEntity();
            RoundEntities = new List<RoundEntity>();
            JudgeEntities = new List<JudgeUserEntity>();
            ContestantEntities = new List<ContestantEntity>();
            SelectedContestant = null;

            SetupRecorder.Add("AdministerEventCache");
            ApplicationLogger.LogInformationMessage("'AdministerEventCache' setup complete");
        }

        public static void Clear()
        {
            EventLayoutSequence = new EventLayoutSequenceEntity();
            RoundEntities = new List<RoundEntity>();
            JudgeEntities = new List<JudgeUserEntity>();
            ContestantEntities = new List<ContestantEntity>();
            SelectedContestant = null;
        }
    }
}

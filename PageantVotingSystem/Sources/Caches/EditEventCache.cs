
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Caches
{
    public class EditEventCache : Cache
    {
        public static EventEntity EventEntity { get; private set; }

        public static GenericOrderedList<string> JudgeEntities { get; private set; }

        public static GenericOrderedList<ContestantEntity> ContestantEntities { get; private set; }

        public static List<string> ContestantFullNames
        {
            get
            {
                List<string> contestantFullNames = new List<string>();
                foreach (ContestantEntity contestantEntity in ContestantEntities.Items)
                {
                    contestantFullNames.Add(contestantEntity.FullName);
                }
                return contestantFullNames;
            }

            private set { }
        }

        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("EditEventCache");
            ApplicationLogger.LogInformationMessage("'EditEventCache' setup began");

            EventEntity = new EventEntity();
            JudgeEntities = new GenericOrderedList<string>();
            ContestantEntities = new GenericOrderedList<ContestantEntity>();

            SetupRecorder.Add("EditEventCache");
            ApplicationLogger.LogInformationMessage("'EditEventCache' setup complete");
        }

        public static void Clear()
        {
            EventEntity.ClearAllAttributes();
            JudgeEntities.ClearAllItems();
            ContestantEntities.ClearAllItems();
        }
    }
}

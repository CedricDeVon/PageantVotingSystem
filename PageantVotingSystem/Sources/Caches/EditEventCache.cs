
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Caches
{
    public class EditEventCache : Cache
    {
        public static EventEntity Event { get; private set; }

        public static GenericOrderedList<string> Judges { get; private set; }

        public static GenericOrderedList<ContestantEntity> Contestants { get; private set; }

        public static List<string> ContestantFullNames
        {
            get
            {
                List<string> contestantFullNames = new List<string>();
                foreach (ContestantEntity contestantEntity in Contestants.Items)
                {
                    contestantFullNames.Add(contestantEntity.FullName);
                }
                return contestantFullNames;
            }

            private set { }
        }

        public static void Setup(
            EventEntity @event,
            GenericOrderedList<string> judges,
            GenericOrderedList<ContestantEntity> contestants)
        {
            SetupRecorder.ThrowIfAlreadySetup("EditEventCache");

            Event = @event;
            Judges = judges;
            Contestants = contestants;

            SetupRecorder.Add("EditEventCache");
        }

        public static void ClearAllAttributes()
        {
            Event.ClearAllAttributes();
            Judges.ClearAllItems();
            Contestants.ClearAllItems();
        }
    }
}


using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Caches
{
    public class EditEventContestantCache
    {
        public static List<ContestantEntity> SelectedContestants
        {
            get { return selectedContestants.ToList(); }

            private set { }
        }

        private static List<ContestantEntity> selectedContestants;

        public static void Setup(List<ContestantEntity> contestants)
        {
            SetupRecorder.ThrowIfAlreadySetup("EditEventContestantCache");

            selectedContestants = contestants;

            SetupRecorder.Add("EditEventContestantCache");
        }

        public static Result AddContestant(ContestantEntity contestant)
        {
            selectedContestants.Add(contestant);
            return new ResultSuccess();
        }

        public static void MoveContestantAtIndexDownwards(int index)
        {
            if (index <= 0)
            {
                return;
            }

            int newIndex = index - 1;
            (selectedContestants[index], selectedContestants[newIndex]) = (selectedContestants[newIndex], selectedContestants[index]);
        }

        public static void MoveContestantAtIndexUpwards(int index)
        {
            if (index >= selectedContestants.Count - 1)
            {
                return;
            }

            int newIndex = index + 1;
            (selectedContestants[index], selectedContestants[newIndex]) = (selectedContestants[newIndex], selectedContestants[index]);
        }

        public static void RemoveContestantAtIndex(int index)
        {
            if (-1 > index || index > selectedContestants.Count - 1)
            {
                return;
            }

            selectedContestants.RemoveAt(index);
        }

        public static void ClearAllContestantEmails()
        {
            selectedContestants.Clear();
        }
    }
}

using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.Caches
{
    public class EditEventJudgesCache
    {
        public static List<string> SelectedJudgeEmails
        {
            get { return selectedJudgeEmails.ToList(); }

            private set { }
        }

        private static List<string> selectedJudgeEmails;

        private static HashSet<string> uniqueJudgeEmails;

        public static void Setup(List<string> judgeEmails)
        {
            SetupRecorder.ThrowIfAlreadySetup("EditEventJudgesCache");

            selectedJudgeEmails = judgeEmails;
            uniqueJudgeEmails = judgeEmails.ToHashSet();

            SetupRecorder.Add("EditEventJudgesCache");
        }

        public static Result AddJudge(string judgeEmail)
        {
            if (uniqueJudgeEmails.Contains(judgeEmail))
            {
                return new ResultFailed($"'EditEventJudgesCache' - Judge email '{judgeEmail}' already exists");
            }

            selectedJudgeEmails.Add(judgeEmail);
            uniqueJudgeEmails.Add(judgeEmail);
            return new ResultSuccess();
        }

        public static void MoveJudgeAtIndexDownwards(int index)
        {
            if (index <= 0)
            {
                return;
            }

            int newIndex = index - 1;
            (selectedJudgeEmails[index], selectedJudgeEmails[newIndex]) = (selectedJudgeEmails[newIndex], selectedJudgeEmails[index]);
        }

        public static void MoveJudgeAtIndexUpwards(int index)
        {
            if (index >= selectedJudgeEmails.Count - 1)
            {
                return;
            }

            int newIndex = index + 1;
            (selectedJudgeEmails[index], selectedJudgeEmails[newIndex]) = (selectedJudgeEmails[newIndex], selectedJudgeEmails[index]);
        }

        public static void RemoveJudgeAtIndex(int index)
        {
            if (-1 > index || index > selectedJudgeEmails.Count - 1)
            {
                return;
            }

            string temporaryJudgeEmail = selectedJudgeEmails[index];
            selectedJudgeEmails.RemoveAt(index);
            uniqueJudgeEmails.Remove(temporaryJudgeEmail);
        }

        public static void ClearAllJudgeEmails()
        {
            selectedJudgeEmails.Clear();
            uniqueJudgeEmails.Clear();
        }
    }
}

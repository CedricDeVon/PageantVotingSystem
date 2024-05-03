
namespace PageantVotingSystem.Sources.Entities
{
    public class JudgeUserEntity : UserEntity
    {
        public int OrderNumber { get; set; }

        public string JudgeStatusType { get; set; }

        public string RoundContestantStatusType { get; set; }

        public JudgeUserEntity(
            string email,
            int orderNumber,
            string fullName)
        {
            OrderNumber = orderNumber;
            Email = email;
            FullName = fullName;
        }

        public JudgeUserEntity(
            int orderNumber,
            string email,
            string roundContestantStatusType)
        {
            OrderNumber = orderNumber;
            Email = email;
            RoundContestantStatusType = roundContestantStatusType;
        }

        public JudgeUserEntity(
            int orderNumber,
            string email,
            string fullName,
            string roundContestantStatusType)
        {
            OrderNumber = orderNumber;
            Email = email;
            FullName = fullName;
            RoundContestantStatusType = roundContestantStatusType;
        }

        public override void ClearAllAttributes()
        {
            SetAllAttributesToDefault();

            OrderNumber = 0;
            RoundContestantStatusType = "";
        }
    }
}

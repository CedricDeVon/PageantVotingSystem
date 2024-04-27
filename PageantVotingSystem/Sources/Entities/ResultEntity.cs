
namespace PageantVotingSystem.Sources.Entities
{
    public class ResultEntity : Entity
    {
        public int EventId { get; set; }

        public int SegmentId { get; set; }

        public int RoundId { get; set; }

        public int CriteriumId { get; set; }

        public float BaseValue { get; set; }

        public string ResultRemarkType { get; set; }

        public int ContestantId { get; set; }

        public string JudgeUserEmail { get; set; }

        public ResultEntity()
        {
            SetAllAttributes();
        }

        public override void ClearAllAttributes()
        {
            SetAllAttributes();
        }

        private void SetAllAttributes(
            int eventId = 0,
            int segmentId = 0,
            int roundId = 0,
            int criteriumId = 0,
            float baseValue = 0,
            string resultRemarkType = "",
            int contestantId = 0,
            string judgeUserEmail = "")
        {
            EventId = eventId;
            SegmentId = segmentId;
            RoundId = roundId;
            CriteriumId = criteriumId;
            BaseValue = baseValue;
            ResultRemarkType = resultRemarkType;
            ContestantId = contestantId;
            JudgeUserEmail = judgeUserEmail;

        }
    }
}

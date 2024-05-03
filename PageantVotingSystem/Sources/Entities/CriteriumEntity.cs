
namespace PageantVotingSystem.Sources.Entities
{
    public class CriteriumEntity : EventLayoutItemEntity
    {
        public float MaximumValue { get; set; }

        public float MinimumValue { get; set; }

        public float PercentageWeight { get; set; }

        public int RoundId { get; set; }

        public CriteriumEntity()
        {
            SetAllAttributes();
        }

        public CriteriumEntity(int id)
        {
            SetAllAttributes();
            Id = id;
        }

        public CriteriumEntity(string name)
        {
            SetAllAttributes();
            Name = name;
        }
        
        public CriteriumEntity(int id, string name)
        {
            SetAllAttributes();
            Id = id;
            Name = name;
        }

        public override void ClearAllAttributes()
        {
            base.ClearAllAttributes();

            SetAllAttributes();
        }

        private void SetAllAttributes(
            float maximumValue = 100,
            float minimumValue = 0,
            float percentageValue = 100,
            int roundId = 0)
        {
            MaximumValue = maximumValue;
            MinimumValue = minimumValue;
            PercentageWeight = percentageValue;
            RoundId = roundId;
        }
    }
}

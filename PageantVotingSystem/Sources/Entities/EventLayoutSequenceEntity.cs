
namespace PageantVotingSystem.Sources.Entities
{
    public class EventLayoutSequenceEntity
    {
        public EventEntity Event { get; set; }

        public SegmentEntity Segment { get; set; }

        public RoundEntity Round { get; set; }

        public CriteriumEntity Criterium { get; set; }

        public EventLayoutSequenceEntity()
        {
            Event = new EventEntity();
            Segment = new SegmentEntity();
            Round = new RoundEntity();
            Criterium = new CriteriumEntity();
        }
    }
}

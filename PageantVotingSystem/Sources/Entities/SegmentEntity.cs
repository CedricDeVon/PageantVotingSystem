
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Entities
{
    public class SegmentEntity : EventLayoutItemEntity
    {
        public int MaximumContestantCount { get; set; }

        public int EventId { get; set; }

        public GenericOrderedList<RoundEntity> Rounds { get; private set; }
        
        public List<string> RoundNamesInNormalOrder
        {
            get
            {
                List<string> roundNames = new List<string>();
                List<RoundEntity> roundEntities = Rounds.Items;
                for (int index = 0; index < Rounds.ItemCount; index++)
                {
                    RoundEntity currentRoundEntity = roundEntities[index];
                    roundNames.Add(currentRoundEntity.Name);
                }
                return roundNames;
            }

            private set { }
        }

        public List<string> RoundNamesInReverseOrder
        {
            get
            {
                List<string> roundNames = new List<string>();
                List<RoundEntity> roundEntities = Rounds.Items;
                for (int index = Rounds.ItemCount - 1; index > -1; index--)
                {
                    RoundEntity currentRoundEntity = roundEntities[index];
                    roundNames.Add(currentRoundEntity.Name);
                }
                return roundNames;
            }

            private set { }
        }

        public SegmentEntity()
        {
            SetAllAttributes();
            Rounds = new GenericOrderedList<RoundEntity>();
        }
        
        public SegmentEntity(string name)
        {
            SetAllAttributes();
            Rounds = new GenericOrderedList<RoundEntity>();
            Name = name;
        }

        public SegmentEntity(int id, string name)
        {
            SetAllAttributes();
            Rounds = new GenericOrderedList<RoundEntity>();
            Id = id;
            Name = name;
        }

        public override void ClearAllAttributes()
        {
            base.ClearAllAttributes();

            SetAllAttributes();
            Rounds.ClearAllItems();
        }

        private void SetAllAttributes(
            int maximumContestantCount = 0,
            int eventId = 0)
        {
            MaximumContestantCount = maximumContestantCount;
            EventId = eventId;
        }
    }
}

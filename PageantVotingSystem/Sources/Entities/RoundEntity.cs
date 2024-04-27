
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Entities
{
    public class RoundEntity : EventLayoutEntity
    {
        public int SegmentId { get; set; }

        public GenericOrderedList<CriteriumEntity> Criteria { get; private set; }

        public List<string> CriteriumNamesInNormalOrder
        {
            get
            {
                List<string> criteriumNames = new List<string>();
                List<CriteriumEntity> criteriumEntities = Criteria.Items;
                for (int index = 0; index < Criteria.ItemCount; index++)
                {
                    CriteriumEntity currentCriteriumEntity = criteriumEntities[index];
                    criteriumNames.Add(currentCriteriumEntity.Name);
                }
                return criteriumNames;
            }

            private set { }
        }

        public List<string> CriteriumNamesInReverseOrder
        {
            get
            {
                List<string> criteriumNames = new List<string>();
                List<CriteriumEntity> criteriumEntities = Criteria.Items;
                for (int index = Criteria.ItemCount - 1; index > -1; index--)
                {
                    CriteriumEntity currentCriteriumEntity = criteriumEntities[index];
                    criteriumNames.Add(currentCriteriumEntity.Name);
                }
                return criteriumNames;
            }

            private set { }
        }

        public RoundEntity()
        {
            SetAllAttributes();
            Criteria = new GenericOrderedList<CriteriumEntity>();
        }

        public RoundEntity(int id)
        {
            SetAllAttributes();
            Criteria = new GenericOrderedList<CriteriumEntity>();
            Id = id;
        }

        public RoundEntity(string name)
        {
            SetAllAttributes();
            Criteria = new GenericOrderedList<CriteriumEntity>();
            Name = name;
        }
        
        public RoundEntity(int id, string name)
        {
            SetAllAttributes();
            Criteria = new GenericOrderedList<CriteriumEntity>();
            Id = id;
            Name = name;
        }

        public override void ClearAllAttributes()
        {
            base.ClearAllAttributes();

            SetAllAttributes();
            Criteria.ClearAllItems();
        }

        private void SetAllAttributes(
            int segmentId = 0)
        {
            SegmentId = segmentId;
        }
    }
}

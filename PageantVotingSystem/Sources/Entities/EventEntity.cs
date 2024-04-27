
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Entities
{
    public class EventEntity : EventLayoutEntity
    {
        public string ManagerEmail { get; set; }

        public string HostAddress { get; set; }

        public string DateTimeStart { get; set; }

        public string DateTimeEnd { get; set; }

        public string ScoringSystemType { get; set; }

        public GenericOrderedList<SegmentEntity> Segments { get; private set; }

        public List<string> SegmentNamesInNormalOrder
        {
            get
            {
                List<string> segmentNames = new List<string>();
                List<SegmentEntity> segmentEntities = Segments.Items;
                for (int index = 0; index < Segments.ItemCount; index++)
                {
                    SegmentEntity currentSegmentEntity = segmentEntities[index];
                    segmentNames.Add(currentSegmentEntity.Name);
                }
                return segmentNames;
            }

            private set { }
        }

        public List<string> SegmentNamesInReverseOrder
        {
            get
            {
                List<string> segmentNames = new List<string>();
                List<SegmentEntity> segmentEntities = Segments.Items;
                for (int index = Segments.ItemCount - 1; index > -1; index--)
                {
                    SegmentEntity currentSegmentEntity = segmentEntities[index];
                    segmentNames.Add(currentSegmentEntity.Name);
                }
                return segmentNames;
            }

            private set { }
        }

        public EventEntity()
        {
            SetAllAttributes(DateTime.Now.ToString());
            Segments = new GenericOrderedList<SegmentEntity>();
        }

        public EventEntity(int id)
        {
            SetAllAttributes(DateTime.Now.ToString());
            Segments = new GenericOrderedList<SegmentEntity>();
            Id = id;
        }

        public EventEntity(string name)
        {
            SetAllAttributes(DateTime.Now.ToString());
            Segments = new GenericOrderedList<SegmentEntity>();
            Name = name;
        }

        public EventEntity(int id, string name)
        {
            SetAllAttributes(DateTime.Now.ToString());
            Segments = new GenericOrderedList<SegmentEntity>();
            Id = id;
            Name = name;
        }

        public void Display()
        {
            Console.WriteLine(Name);
            foreach (SegmentEntity entity in Segments.Items)
            {
                Display(entity);
            }
        }

        private void Display(SegmentEntity entity)
        {
            Console.WriteLine($"\t{entity.Name}");
            foreach (RoundEntity e in entity.Rounds.Items)
            {
                Display(e);
            }
        }

        private void Display(RoundEntity entity)
        {
            Console.WriteLine($"\t\t{entity.Name}");
            foreach (CriteriumEntity e in entity.Criteria.Items)
            {
                Display(e);
            }
        }

        private void Display(CriteriumEntity entity)
        {
            Console.WriteLine($"\t\t\t{entity.Name}");
        }

        public override void ClearAllAttributes()
        {
            base.ClearAllAttributes();

            SetAllAttributes(DateTime.Now.ToString());
            Segments.ClearAllItems();
        }

        private void SetAllAttributes(
            string dateTimeStart = "",
            string dateTimeEnd = "",
            string hostAddress = "",
            string scoringSystemType = "")
        {
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
            HostAddress = hostAddress;
            ScoringSystemType = scoringSystemType;
        }
    }
}

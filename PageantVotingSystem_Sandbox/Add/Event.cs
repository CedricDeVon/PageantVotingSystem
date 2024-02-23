using System;
using System.Collections.Generic;

namespace PageantVotingSystem
{
    public class Event
    {
        private int EventId { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string Address { get; set; }
        private string DateScheduled { get; set; }
        private string ScoringSystemType { get; set; }

        private List<Segment> segment { get; set; }
        public Event(int EventId, string Name, string Description, string Address, string DateScheduled, string ScoringSystemType)
        {
            this.EventId = EventId;
            this.Name = Name;
            this.Description = Description;
            this.Address = Address;
            this.DateScheduled = DateScheduled;
            this.ScoringSystemType = ScoringSystemType;
            segment = new List<Segment>();
        }
        public void DisplayEvent()
        {
            Console.WriteLine("\n\tEvents\n");
            Console.WriteLine("Event ID: " + this.EventId);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Description: " + this.Description);
            Console.WriteLine("Address: " + this.Address);
            Console.WriteLine("Date Schedule: " + this.DateScheduled);
            Console.WriteLine("Scoring System Type: " + this.ScoringSystemType);
            DisplayList(this.segment);

        }
        public void AddSegment(Segment segment)
        {
            this.segment.Add(segment);
        }
        public void DisplayList(List<Segment> Segments)
        {
            foreach (var segments in Segments)
            {
                segments.DisplaySegment();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace PageantVotingSystem
{

    public class Segment
    {
        private int SegmentId { get; set; }
        private string SegmentName { get; set; }
        private string SegmentDescription { get; set; }
        private double PercentageWeight { get; set; }

        public List<Round> Round { get; set; }
        public Segment(int SegmentId, string SegmentName, string SegmentDescription, double PercentageWeight)
        {
            this.SegmentId = SegmentId;
            this.SegmentName = SegmentName;
            this.SegmentDescription = SegmentDescription;
            this.PercentageWeight = PercentageWeight;
            this.Round = new List<Round>();
        }

        public void AddRound(Round round)
        {
            this.Round.Add(round);
        }
        public void DisplaySegment()
        {
            Console.WriteLine("\n\tSegments\n");
            Console.WriteLine("Round ID: " + this.SegmentId);
            Console.WriteLine("Name: " + this.SegmentName);
            Console.WriteLine("Description: " + this.SegmentDescription);
            Console.WriteLine("Weight: " + this.PercentageWeight);
            DisplayList(Round);
        }
        public void DisplayList(List<Round> round)
        {
            foreach (var roundy in round)
            {
                roundy.DisplayRound();
            }
        }
    }
}

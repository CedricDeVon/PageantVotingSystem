using System;
using System.Collections.Generic;

namespace PageantVotingSystem
{
    public class Round
    {
        private int RoundId { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private double Weight { get; set; }

        public List<Criteria> criteria { get; set; }

        public Round(int RoundId, string Name, string Description, double Weight)
        {
            this.RoundId = RoundId;
            this.Name = Name;
            this.Description = Description;
            this.Weight = Weight;
            this.criteria = new List<Criteria>();
        }
        public void DisplayRound()
        {

            Console.WriteLine("\n\tRound\n");
            Console.WriteLine("Round ID: " + this.RoundId);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Description: " + this.Description);
            Console.WriteLine("Weight: " + this.Weight);
            DisplayList(criteria);
        }
        public void AddCriteria(Criteria criteria)
        {
            this.criteria.Add(criteria);
        }
        public void DisplayList(List<Criteria> criteria)
        {
            foreach (Criteria member in criteria)
            {
                member.DisplayCriteria();
            }
        }

    }
}

using System;

namespace PageantVotingSystem
{
    public class Criteria
    {
        private int CriteriaId { get; set; }
        private string CriteriaName { get; set; }
        private double CriteriaPercentageWeight { get; set; }
        private double CriteriaBaseValue { get; set; }

        public Criteria(int CriteriaId, string CriteriaName, double CriteriaPercentageWeight, double CriteriaBaseValue)
        {
            this.CriteriaId = CriteriaId;
            this.CriteriaName = CriteriaName;
            this.CriteriaPercentageWeight = CriteriaPercentageWeight;
            this.CriteriaBaseValue = CriteriaBaseValue;
        }
        public void DisplayCriteria()
        {
            Console.WriteLine("\n\tCriteria\n");
            Console.WriteLine("Criteria ID: " + this.CriteriaId);
            Console.WriteLine("Criteria Name: " + this.CriteriaName);
            Console.WriteLine("Weight: " + this.CriteriaPercentageWeight);
            Console.WriteLine("Score: " + this.CriteriaBaseValue);
        }

    }
}

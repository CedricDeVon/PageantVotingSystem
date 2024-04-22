
namespace PageantVotingSystem.Sources.Entities
{
    public class ContestantResultEntity : Entity
    {
        public string RankingLabel
        {
            get
            {
                if (RankingNumber % 10 == 1)
                {
                    return $"{RankingNumber}st";
                }
                else if (RankingNumber % 10 == 2)
                {
                    return $"{RankingNumber}nd";
                }
                else if (RankingNumber % 10 == 3)
                {
                    return $"{RankingNumber}rd";
                }
                return $"{RankingNumber}th";
            }

            private set { }
        }

        public int RankingNumber { get; set; }

        public int OrderNumber { get; set; } 

        public string FullName { get; set; }

        public float NetPercentage { get; set; }

        public string NetPercentageLabel
        {
            get { return $"{NetPercentage} %"; }

            private set { }
        }

        public float NetValue { get; set; }

        public float MaximumValue { get; set; }

        public string ValueLabel
        {
            get { return $"{NetValue} / {MaximumValue}"; }

            private set { }
        }

        public ContestantResultEntity(
            int rankingNumber,
            int orderNumber,
            string fullName,
            float netPercentage,
            float netValue,
            float maximumValue)
        {
            RankingNumber = rankingNumber;
            OrderNumber = orderNumber;
            FullName = fullName;
            NetPercentage = netPercentage;
            NetValue = netValue;
            MaximumValue = maximumValue;
        }

        public override void ClearAllAttributes()
        {
            RankingNumber = 0;
            OrderNumber = 0;
            FullName = "";
            NetPercentage = 0;
            NetValue = 0;
            MaximumValue = 0;
        }
    }
}

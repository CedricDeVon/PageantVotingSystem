namespace PageantVotingSystem
{
    public class Contestant
    {
        private int Id { get; set; }
        private string FullName { get; set; }
        private int Age { get; set; }
        public Contestant(int Id, string FullName, int Age)
        {
            this.Id = Id;
            this.FullName = FullName;
            this.Age = Age;
        }

    }
}

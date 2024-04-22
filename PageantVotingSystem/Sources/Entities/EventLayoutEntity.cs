
namespace PageantVotingSystem.Sources.Entities
{
    public abstract class EventLayoutEntity : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public EventLayoutEntity()
        {
            SetAllAttributes();
        }

        public override void ClearAllAttributes()
        {
            SetAllAttributes();
        }

        private void SetAllAttributes(
            int id = 0,
            string name = "",
            string description = "")
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}

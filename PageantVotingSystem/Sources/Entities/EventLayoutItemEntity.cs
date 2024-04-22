
namespace PageantVotingSystem.Sources.Entities
{
    public abstract class EventLayoutItemEntity : EventLayoutEntity
    {
        public int OrderNumber { get; set; }

        public EventLayoutItemEntity()
        {
            SetAllAttributes();
        }

        public override void ClearAllAttributes()
        {
            base.ClearAllAttributes();

            SetAllAttributes();
        }

        private void SetAllAttributes(
            int orderNumber = 0)
        {
            OrderNumber = orderNumber;
        }
    }
}

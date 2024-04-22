
namespace PageantVotingSystem.Sources.Generics
{
    public class GenericDoublyLinkedListItem : Generic
    {
        public object Value
        {
            get { return Data; }

            set { Data = value; }
        }

        public GenericDoublyLinkedListItem NextItem { get; set; }

        public GenericDoublyLinkedListItem PreviousItem { get; set; }

        public GenericDoublyLinkedListItem(object value) : base(value) { }

        public static Type GetNextItemValue<Type>(GenericDoublyLinkedListItem item)
        {
            return (item.NextItem != null) ? (Type)item.NextItem.Value : default;
        }

        public static Type GetPreviousItemValue<Type>(GenericDoublyLinkedListItem item)
        {
            return (item.PreviousItem != null) ? (Type)item.PreviousItem.Value : default;
        }
    }
}

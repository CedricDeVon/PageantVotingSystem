
using System;

namespace PageantVotingSystem.Sources.Generics
{
    public class GenericDoublyLinkedList
    {
        public int Count { get; private set; }

        public object FirstItemValue
        {
            get { return firstItem.Value; }

            private set { }
        }

        public object LastItemValue
        {
            get { return lastItem.Value; }

            private set { }
        }

        public GenericDoublyLinkedListItem FirstItem
        {
            get { return firstItem; }

            private set { }
        }

        public GenericDoublyLinkedListItem LastItem
        {
            get { return lastItem; }

            private set { }
        }

        private GenericDoublyLinkedListItem firstItem;

        private GenericDoublyLinkedListItem lastItem;

        public bool IsEmpty()
        {
            return firstItem == null && lastItem == null;
        }

        public bool IsNotEmpty()
        {
            return !IsEmpty();
        }

        public void AddToFirst(GenericDoublyLinkedListItem newItem)
        {
            if (newItem == null)
            {
                throw new Exception("'GenericDoublyLinkedList' - 'newItem' cannot be null");
            }

            if (firstItem == null && lastItem == null)
            {
                firstItem = newItem;
                lastItem = newItem;
            }
            else
            {
                newItem.NextItem = firstItem;
                firstItem.PreviousItem = newItem;
                firstItem = newItem;
            }
            Count += 1;
        }

        public void AddToLast(GenericDoublyLinkedListItem newItem)
        {
            if (newItem == null)
            {
                throw new Exception("'GenericDoublyLinkedList' - 'newItem' cannot be null");
            }

            if (firstItem == null && lastItem == null)
            {
                firstItem = newItem;
                lastItem = newItem;
            }
            else
            {
                newItem.PreviousItem = lastItem;
                lastItem.NextItem = newItem;
                lastItem = newItem;
            }
            Count += 1;
        }

        public Type RemoveFirst<Type>()
        {
            if (Count <= 0)
            {
                return default;
            }

            Type targetValue = (Type) firstItem.Value;
            if (firstItem == lastItem)
            {
                firstItem = null;
                lastItem = null;
            }
            else
            {
                GenericDoublyLinkedListItem targetItem = firstItem;
                firstItem = firstItem.NextItem;
                firstItem.PreviousItem = null;
                targetItem.NextItem = null;
            }
            Count -= 1;
            return targetValue;
        }

        public Type RemoveLast<Type>()
        {
            if (Count <= 0)
            {
                return default;
            }

            Type targetValue = (Type) lastItem.Value;
            if (firstItem == lastItem)
            {
                firstItem = null;
                lastItem = null;
            }
            else
            {
                GenericDoublyLinkedListItem targetItem = lastItem;
                lastItem = lastItem.PreviousItem;
                lastItem.NextItem = null;
                targetItem.PreviousItem = null;
            }
            Count -= 1;
            return targetValue;
        }

        public Type RemoveItem<Type>(GenericDoublyLinkedListItem targetItem)
        {
            if (targetItem == null)
            {
                throw new Exception("'GenericDoublyLinkedList' - 'newItem' cannot be null");
            }

            if (Count <= 0)
            {
                return default;
            }

            Type targetValue = (Type) targetItem.Value;
            if (firstItem == lastItem)
            {
                firstItem = null;
                lastItem = null;
                Count -= 1;
            }
            else if (firstItem == targetItem)
            {
                return RemoveFirst<Type>();
            }
            else if (lastItem == targetItem)
            {
                return RemoveLast<Type>();
            }
            else
            {
                GenericDoublyLinkedListItem firstItem = targetItem.NextItem;
                GenericDoublyLinkedListItem secondItem = targetItem.PreviousItem;
                firstItem.PreviousItem = secondItem;
                secondItem.NextItem = firstItem;
                Count -= 1;
            }
            return targetValue;
        }
    }
}

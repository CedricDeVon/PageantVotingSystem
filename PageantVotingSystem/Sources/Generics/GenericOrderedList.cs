
using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.Generics
{
    public class GenericOrderedList<ItemType>
    {
        public List<ItemType> Items
        {
            get { return items; }

            private set { }
        }

        public int ItemCount
        {
            get { return items.Count; }

            private set { }
        }

        private readonly List<ItemType> items;

        public GenericOrderedList()
        {
            items = new List<ItemType>();
        }

        public bool IsItemFound(ItemType item)
        {
            return items.Contains(item);
        }

        public bool IsItemNotFound(ItemType item)
        {
            return !IsItemFound(item);
        }

        public ItemType GetItemAtIndex(int index)
        {
            if (index < 0 || index >= ItemCount)
            {
                throw new Exception("'GenericOrderedList' - index is out of bounds");
            }

            return items[index];
        }

        public void AddNewItem(ItemType item)
        {
            items.Add(item);
        }

        public void MoveItemAtIndexDownwards(int index)
        {
            if (index < 1)
            {
                return;
            }

            int newIndex = index - 1;
            (items[index], items[newIndex]) = (items[newIndex], items[index]);
        }

        public void MoveItemAtIndexUpwards(int index)
        {
            if (index > items.Count - 2)
            {
                return;
            }

            int newIndex = index + 1;
            (items[index], items[newIndex]) = (items[newIndex], items[index]);
        }

        public void RemoveItemAtIndex(int index)
        {
            if (-1 > index || index > items.Count - 1)
            {
                return;
            }

            items.RemoveAt(index);
        }

        public void RemoveItem(ItemType item)
        {
            if (items.Count <= 0)
            {
                return;
            }

            items.Remove(item);
        }

        public void ClearAllItems()
        {
            items.Clear();
        }
    }
}

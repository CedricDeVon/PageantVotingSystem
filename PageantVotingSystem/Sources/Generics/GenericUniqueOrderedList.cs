
using System.Collections.Generic;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace PageantVotingSystem.Sources.Generics
{
    public class GenericUniqueOrderedList<KeyType, ValueType>
    {
        public List<KeyType> Keys
        {
            get { return keys; }

            private set { }
        }

        public Dictionary<KeyType, ValueType> Items
        {
            get { return items; }

            private set { }
        }

        public int ItemCount
        {
            get { return keys.Count; }
            
            private set { }
        }

        private readonly List<KeyType> keys;

        private readonly Dictionary<KeyType, ValueType> items;

        public GenericUniqueOrderedList()
        {
            keys = new List<KeyType>();
            items = new Dictionary<KeyType, ValueType>();
        }

        public bool IsItemKeyFound(KeyType key)
        {
            return items.ContainsKey(key);
        }

        public bool IsItemValueFound(ValueType value)
        {
            return items.ContainsValue(value);
        }

        public bool IsItemKeyNotFound(KeyType key)
        {
            return !IsItemKeyFound(key);
        }

        public bool IsItemValueNotFound(ValueType value)
        {
            return !IsItemValueFound(value);
        }

        public KeyType GetItemKeyAtIndex(int index)
        {
            if (index < -1 || index >= items.Count)
            {
                return default;
            }

            return keys[index];
        }

        public ValueType GetItemValueAtIndex(int index)
        {
            if (index < -1 || index >= items.Count)
            {
                return default;
            }

            return items[keys[index]];
        }

        public void AddNewItem(KeyType key, ValueType value)
        {
            if (items.ContainsKey(key))
            {
                return;
            }

            keys.Add(key);
            items[key] = value;
        }

        public void MoveItemAtIndexDownwards(int index)
        {
            if (index < 1)
            {
                return;
            }

            int newIndex = index - 1;
            (keys[index], keys[newIndex]) = (keys[newIndex], keys[index]);
        }

        public void MoveItemAtIndexUpwards(int index)
        {
            if (index > keys.Count - 2)
            {
                return;
            }

            int newIndex = index + 1;
            (keys[index], keys[newIndex]) = (keys[newIndex], keys[index]);
        }

        public void RemoveItemAtIndex(int index)
        {
            if (-1 > index || index > keys.Count - 1)
            {
                return;
            }

            KeyType targetKey = keys[index];
            keys.RemoveAt(index);
            items.Remove(targetKey);
        }

        public void RemoveItem(KeyType key)
        {
            if (keys.Count <= 0)
            {
                return;
            }

            keys.Remove(key);
            items.Remove(key);
        }

        public void UpdateItemKey(KeyType oldKey, KeyType newKey)
        {
            if (!items.ContainsKey(oldKey) || items.ContainsKey(newKey))
            {
                return;
            }

            ValueType value = items[oldKey];
            items.Remove(oldKey);
            items[newKey] = value;
            int index = keys.IndexOf(oldKey);
            keys[index] = newKey;
        }

        public void ClearAllItems()
        {
            keys.Clear();
            items.Clear();
        }
    }
}

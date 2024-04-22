using System;
using System.Collections.Generic;

using PageantVotingSystem.Source.Caches;

namespace PageantVotingSystem.Source.Utilities
{
    public class GenericData
    {
        private readonly object Data;

        public GenericData(object data)
        {
            Data = data;
        }

        public Type Get<Type>()
        {
            try
            {
                return (Type)Data;
            }
            catch
            {
                throw new Exception($"'GenericData' type conversion failed");
            }
        }
        public CacheData Key(object key)
        {
            try
            {
                return new CacheData(((Dictionary<object, object>)Data)[key]);
            }
            catch
            {
                throw new Exception($"'GenericData' is not a Dictionary nor is '{key}' an existing key");
            }
        }

        public Type Key<Type>(object key)
        {
            try
            {
                return (Type)((Dictionary<object, object>)Data)[key];
            }
            catch
            {
                throw new Exception($"'GenericData' is not a Dictionary nor is '{key}' an existing key");
            }
        }

        public CacheData Index(int index)
        {
            try
            {
                return new CacheData(((List<object>)Data)[index]);
            }
            catch
            {
                throw new Exception($"'GenericData' is not a List nor is '{index}' a valid index");
            }
        }

        public Type Index<Type>(int index)
        {
            try
            {
                return (Type)((List<object>)Data)[index];
            }
            catch
            {
                throw new Exception($"'GenericData' is not a List nor is '{index}' a valid index");
            }
        }
    }
}

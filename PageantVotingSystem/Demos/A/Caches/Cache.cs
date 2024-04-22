using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Source.Caches
{
    public class Cache
    {
        protected static readonly Dictionary<object, object> data = new Dictionary<object, object>();

        public static void Set(object key, object value)
        {
            try
            {
                data[key] = value;
            }
            catch
            {
                throw new Exception($"'Cache' Invalid Write Operation");
            }
        }

        public static CacheData Get(object key)
        {
            try
            {
                return new CacheData(data[key]);
            }
            catch
            {
                throw new Exception($"'Cache' Invalid Read Operation");
            }
        }
        
        public static Type Get<Type>(object key)
        {
            try
            {
                return (Type) data[key];
            }
            catch
            {
                throw new Exception($"'Cache' Invalid Read Operation");
            }
        }

        public static void Remove(object key)
        {
            try
            {
                data.Remove(key);
            }
            catch
            {
                throw new Exception($"'Cache' Invalid Remove Operation"); ;
            }
        }

        public static void Clear()
        {
            try
            {
                data.Clear();
            }
            catch
            {
                throw new Exception($"'Cache' Invalid Clear Operation");
            }
        }
    }
}

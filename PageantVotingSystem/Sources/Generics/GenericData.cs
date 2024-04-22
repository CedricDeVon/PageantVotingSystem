
using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.Generics
{
    public class GenericData : Generic
    {
        public GenericData(object data) : base(data) { }

        public Type GetData<Type>()
        {
            try
            {
                return (Type) Data;
            }
            catch
            {
                throw new Exception($"'GenericData' - Type conversion failed");
            }
        }

        public GenericData GetDataViaKey(object key)
        {
            try
            {
                return new GenericData(((Dictionary<object, object>)Data)[key]);
            }
            catch
            {
                throw new Exception($"'GenericData' - Dictionary key '{key}' does not exist");
            }
        }

        public Type GetDataViaKey<Type>(object key)
        {
            try
            {
                return (Type)((Dictionary<object, object>)Data)[key];
            }
            catch
            {
                throw new Exception($"'GenericData' - Dictionary key '{key}' does not exist");
            }
        }

        public GenericData GetDataViaIndex(int index)
        {
            try
            {
                return new GenericData(((List<object>)Data)[index]);
            }
            catch
            {
                throw new Exception($"'GenericData' - List index '{index}' cannot be used");
            }
        }

        public Type GetDataViaIndex<Type>(int index)
        {
            try
            {
                return (Type)((List<object>)Data)[index];
            }
            catch
            {
                throw new Exception($"'GenericData' - List index '{index}' cannot be used");
            }
        }
    }
}

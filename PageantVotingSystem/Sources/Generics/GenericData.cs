
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
    }
}


using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.Results
{
    public abstract class Result
    {
        public bool IsSuccessful { get; protected set; }
        
        public string Message { get; protected set; }

        public List<Dictionary<object, object>> Data
        {
            get { return data.GetData<List<Dictionary<object, object>>>("Data"); }

            private set { }
        }

        private readonly GenericDataCollection data = new GenericDataCollection();

        public Result(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
            SetDataToPrivate();
        }

        public Result(bool isSuccessful, List<Dictionary<object, object>> values)
        {
            if (values == null || values.Contains(null))
            {
                throw new Exception("'Result' - Cannot contain a null 'values' argument");
            }

            IsSuccessful = isSuccessful;
            SetDataToPrivate(values);
        }

        public Result(string message)
        {
            Message = message;
            SetDataToPrivate();
        }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            SetDataToPrivate();
        }

        public Type GetData<Type>(object key)
        {
            try
            {
                return (Type)Data[0][key];
            }
            catch
            {
                throw new Exception($"'Result' - Cannot be accessed via '{key}' key");
            }
        }

        public Type GetData<Type>(int index, object key)
        {
            try
            {
                return (Type)Data[index][key];
            }
            catch
            {
                throw new Exception($"'Result' - Cannot be accessed via '{index}' index nor '{key}' key");
            }
        }

        private void SetDataToPrivate(object value)
        {
            data.SetDataToPrivate("Data", value);
        }

        private void SetDataToPrivate()
        {
            SetDataToPrivate(new List<Dictionary<object, object>>());
        }
    }
}

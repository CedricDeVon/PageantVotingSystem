﻿
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
            data.SetPrivateData("Data", new List<Dictionary<object, object>>());
        }

        public Result(bool isSuccessful, List<Dictionary<object, object>> values)
        {
            IsSuccessful = isSuccessful;
            data.SetPrivateData("Data", values);
        }

        public Result(string message)
        {
            Message = message;
            data.SetPrivateData("Data", new List<Dictionary<object, object>>());
        }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            data.SetPrivateData("Data", new List<Dictionary<object, object>>());
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
    }
}
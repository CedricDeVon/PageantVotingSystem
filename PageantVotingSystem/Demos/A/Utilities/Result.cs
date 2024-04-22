using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Source.Utilities
{
    public class Result
    {
        public bool IsSuccessful { get; private set; }

        public string ExceptionName { get; private set; }

        public string ExceptionMessage { get; private set; }

        public List<Dictionary<string, object>> Data { get; private set; }

        public Result()
        {
            IsSuccessful = true;
            Data = new List<Dictionary<string, object>>();
        }
        
        public Result(string exceptionMessage)
        {
            ExceptionMessage = exceptionMessage;
            Data = new List<Dictionary<string, object>>();
        }

        public Result(Exception exception)
        {
            ExceptionName = exception.Source;
            ExceptionMessage = exception.Message;
            Data = new List<Dictionary<string, object>>();
        }

        public Result(List<Dictionary<string, object>> data)
        {
            IsSuccessful = true;
            Data = data;
        }

        public Type Value<Type>(string key)
        {
            try
            {
                return (Type) Data[0][key];
            }
            catch
            {
                throw new Exception($"'Result' cannot be accessed via '{key}' key");
            }
        }

        public Type Value<Type>(int index, string key)
        {
            try
            {
                return (Type) Data[index][key];
            }
            catch
            {
                throw new Exception($"'Result' cannot be accessed with '{index}' index and '{key}' key");
            }
        }

        public static Result Success()
        {
            return new Result();
        }
        
        public static Result Success(List<Dictionary<string, object>> data)
        {
            return new Result(data);
        }

        public static Result Failure(string exceptionMessage)
        {
            return new Result(exceptionMessage);
        }

        public static Result Failure(Exception exception)
        {
            return new Result(exception);
        }
    }
}

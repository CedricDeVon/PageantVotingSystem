using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Source.Database
{
    public class DatabaseOutput
    {
        public bool IsOk { get; private set; }

        public string ExceptionName { get; private set; }

        public string ExceptionMessage { get; private set; }

        public List<Dictionary<string, object>> Data { get; private set; }

        public DatabaseOutput()
        {
            IsOk = true;
            Data = new List<Dictionary<string, object>>();
        }

        public DatabaseOutput(Exception exception)
        {
            ExceptionName = exception.Source;
            ExceptionMessage = exception.Message;
            Data = new List<Dictionary<string, object>>();
        }

        public DatabaseOutput(List<Dictionary<string, object>> data)
        {
            IsOk = true;
            Data = data;
        }

        public static DatabaseOutput Success()
        {
            return new DatabaseOutput();
        }
        
        public static DatabaseOutput Success(List<Dictionary<string, object>> data)
        {
            return new DatabaseOutput(data);
        }

        public static DatabaseOutput Failure(Exception exception)
        {
            return new DatabaseOutput(exception);
        }
    }
}

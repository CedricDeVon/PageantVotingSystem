
using System;

namespace PageantVotingSystem.Sources.Results
{
    public class ResultFailed : Result
    {
        public string ExceptionName { get; private set; }

        public ResultFailed(string exceptionMessage) : base(exceptionMessage) { }

        public ResultFailed(Exception exception) : base(exception.Message)
        {
            ThrowIfNull(exception);

            ExceptionName = exception.Source;
        }

        private void ThrowIfNull(Exception exception)
        {
            if (exception == null || !(exception is Exception))
            {
                throw new Exception("'ResultFailed' - Exception cannot be null");
            }
        }
    }
}

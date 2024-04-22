
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.Results
{
    public class ResultSuccess : Result
    {
        public ResultSuccess() : base(true) { }
        
        public ResultSuccess(Dictionary<object, object> dataCollection) : base(true, new List<Dictionary<object, object>>() { dataCollection }) { }

        public ResultSuccess(List<Dictionary<object, object>> dataCollections) : base(true, dataCollections) { }

    }
}
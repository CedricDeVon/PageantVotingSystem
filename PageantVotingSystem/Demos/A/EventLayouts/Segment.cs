using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Source.EventLayouts
{
    public class Segment
    {
        public string Name { get; set; }

        public uint MaximumContestantCount { get; set; }

        public string CurrentStatusType { get; set; }


    }
}

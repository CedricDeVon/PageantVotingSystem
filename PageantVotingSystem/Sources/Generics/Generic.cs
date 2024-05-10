
using System;

namespace PageantVotingSystem.Sources.Generics
{
    public abstract class Generic
    {
        protected object Data;

        public Generic(object data)
        {
            if (data == null)
            {
                throw new Exception("'Generic' - 'data' cannot be null");
            }

            Data = data;
        }
    }
}

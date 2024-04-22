
using System;

namespace PageantVotingSystem.Sources.Systems
{
    public class System
    {
        public static string GetEnvironmentValue(string key)
        {
            string environmentValue = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
            ThrowIfEnvironmentVariableDoesNotExist(environmentValue);

            return environmentValue;
        }

        protected static void ThrowIfEnvironmentVariableDoesNotExist(string value)
        {
            if (value == null)
            {
                throw new Exception($"'System' - Environment variable'{value}' does not exist");
            }
        }
    }
}

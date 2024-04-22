
using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.Setups
{
    public class SetupRecorder
    {
        protected readonly static Dictionary<object, bool> data = new Dictionary<object, bool>();

        public static void Add(object key)
        {
            ThrowIfFound(key);

            data[key] = true;
        }

        public static void Remove(object key)
        {
            ThrowIfNotFound(key);

            data.Remove(key);
        }

        public static void SetToTrue(object key)
        {
            ThrowIfNotFound(key);

            data[key] = true;
        }

        public static void SetToFalse(object key)
        {
            ThrowIfNotSetup(key);

            data[key] = false;
        }

        public static void ThrowIfAlreadySetup(object key)
        {
            if (data.ContainsKey(key) && data[key])
            {
                throw new Exception($"'Setup' - '{key}' is already setup");
            }
        }

        public static void ThrowIfNotSetup(object key)
        {
            if (!data.ContainsKey(key) || !data[key])
            {
                throw new Exception($"'Setup' - '{key}' has not yet been setup");
            }
        }

        public static void ThrowIfFound(object key)
        {
            if (data.ContainsKey(key))
            {
                throw new Exception($"'Setup' - '{key}' already exist");
            }
        }

        public static void ThrowIfNotFound(object key)
        {
            if (!data.ContainsKey(key))
            {
                throw new Exception($"'Setup' - '{key}' does not exist");
            }
        }
    }
}


using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.ResourceLoaders
{
    public class ResourceLoader
    {
        private static readonly Dictionary<string, ResourceData> resources = new Dictionary<string, ResourceData>();

        public static bool IsResourceFound(string filePath)
        {
            return resources.ContainsKey(filePath);
        }

        public static bool IsResourceNotFound(string filePath)
        {
            return !IsResourceFound(filePath);
        }

        public static void AddResource(string filePath)
        {
            ThrowIfFileDoesNotExist(filePath);
            ThrowIfResourceAlreadyExist(filePath);

            resources[filePath] = new ResourceData(filePath);
        }

        public static void UpdateResource(string filePath)
        {
            ThrowIfFileDoesNotExist(filePath);
            ThrowIfResourceDoesNotExist(filePath);

            resources[filePath] = new ResourceData(filePath);
        }

        public static void RemoveResource(string filePath)
        {
            ThrowIfResourceDoesNotExist(filePath);

            resources.Remove(filePath);
        }

        public static Bitmap LoadResource(string filePath)
        {
            ThrowIfResourceDoesNotExist(filePath);

            return resources[filePath].Load();
        }

        public static Bitmap ReloadResource(string filePath)
        {
            ThrowIfResourceDoesNotExist(filePath);

            return resources[filePath].Reload();
        }

        public static void UnloadResource(string filePath)
        {
            ThrowIfResourceAlreadyExist(filePath);
            
            resources[filePath] = null;
        }

        protected static void ThrowIfResourceAlreadyExist(string filePath)
        {
            if (resources.ContainsKey(filePath))
            {
                throw new Exception($"'ResourceLoader' - Resource of path '{filePath}' already exists");
            }
        }

        protected static void ThrowIfResourceDoesNotExist(string filePath)
        {
            if (!resources.ContainsKey(filePath))
            {
                throw new Exception($"'ResourceLoader' - Resource of path '{filePath}' does not exist");
            }
        }

        protected static void ThrowIfFileDoesNotExist(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"'ResourceLoader' - File '{filePath}' does not exist");
            }
        }

        public static void ClearAllResources()
        {
            resources.Clear();
        }
    }
}

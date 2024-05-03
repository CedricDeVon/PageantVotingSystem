
using System.Drawing;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.ResourceLoaders
{
    public class ApplicationResourceLoader : ResourceLoader
    {
        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationResourceLoader");
            ApplicationLogger.LogInformationMessage("'ApplicationResourceLoader' setup began");
            
            foreach (object value in values)
            {
                AddResource((string)value);
            }

            SetupRecorder.Add("ApplicationResourceLoader");
            ApplicationLogger.LogInformationMessage("'ApplicationResourceLoader' setup complete");
        }

        public static Bitmap SafeLoadResource(string filePath)
        {
            if (IsResourceNotFound(filePath))
            {
                AddResource(filePath);
            }

            return LoadResource(filePath);
        }
    }
}

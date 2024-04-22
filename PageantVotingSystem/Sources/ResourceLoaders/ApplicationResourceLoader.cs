
using System.Collections.Generic;
using System.Drawing;
using Google.Protobuf.WellKnownTypes;
using PageantVotingSystem.Sources.Setups;

namespace PageantVotingSystem.Sources.ResourceLoaders
{
    public class ApplicationResourceLoader : ResourceLoader
    {
        public static void Setup(HashSet<object> values)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationResourceLoader");

            foreach (object value in values)
            {
                AddResource((string)value);
            }

            SetupRecorder.Add("ApplicationResourceLoader");
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

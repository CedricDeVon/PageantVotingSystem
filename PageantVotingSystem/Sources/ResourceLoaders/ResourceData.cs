
using System;
using System.Drawing;

namespace PageantVotingSystem.Sources.ResourceLoaders
{
    public class ResourceData
    {
        public string FilePath { get; private set; }

        private Bitmap resource;

        public ResourceData(string filePath)
        {
            FilePath = filePath;
        }

        public Bitmap Load()
        {
            if (resource == null)
            {
                resource = GenerateBitmapResource(FilePath);
            }

            return resource;
        }

        public Bitmap Reload()
        {
            resource = GenerateBitmapResource(FilePath);
            return resource;
        }

        private Bitmap GenerateBitmapResource(string filePath)
        {
            try
            {
                return new Bitmap(filePath);
            }
            catch
            {
                throw new Exception($"'ResourceData' - File path '{filePath}' cannot be converted into Bitmap");
            }
        }
    }
}

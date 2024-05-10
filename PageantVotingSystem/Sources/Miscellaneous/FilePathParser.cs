
using System;

namespace PageantVotingSystem.Sources.Miscellaneous
{
    public class FilePathParser
    {
        public static string Standardize(string filePath)
        {
            if (filePath == null)
            {
                throw new Exception("'StringParser' - 'filePath' must not be null");
            }

            string updatedFilePath = "";
            for (int index = 0; index < filePath.Length; index++)
            {
                char character = filePath[index];
                updatedFilePath += (character == '\\') ? '/' : character;
            }
            return updatedFilePath;
        }
    }
}

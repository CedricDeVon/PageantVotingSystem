
namespace PageantVotingSystem.Sources.Miscellaneous
{
    public class StringParser
    {
        public static string StandardizeFilePath(string filePath)
        {
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

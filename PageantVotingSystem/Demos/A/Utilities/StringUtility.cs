
namespace PageantVotingSystem.Source.Utilities
{
    public class StringUtility
    {
        public static bool IsAlphaNumericCharacter(char character)
        {
            return 47 < character && character < 58;
        }

        public static bool IsAlphabeticalCharacter(char character)
        {
            return IsLowerCaseAlphabeticalCharacter(character) || IsUpperCaseAlphabeticalCharacter(character);
        }

        public static bool IsLowerCaseAlphabeticalCharacter(char character)
        {
            return 96 < character && character < 123;
        }

        public static bool IsUpperCaseAlphabeticalCharacter(char character)
        {
            return 64 < character && character < 91;
        }

        public static bool IsSpecialCharacter(char character)
        {
            return !IsAlphaNumericCharacter(character) && !IsLowerCaseAlphabeticalCharacter(character) && !IsUpperCaseAlphabeticalCharacter(character);
        }

        public static bool IsInvalidSpecialCharacter(char character)
        {
            return character == ' ' || character == '\\' || character == '/' || character == '"' || character == '\'' || character == '`';
        }
    }
}

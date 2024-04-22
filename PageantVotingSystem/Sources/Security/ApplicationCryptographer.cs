
using PageantVotingSystem.Sources.Systems;

namespace PageantVotingSystem.Sources.Security
{
    public class ApplicationCryptographer : Cryptographer
    {
        public static string SecurePasswordViaMethod1(string password)
        {
            return EncryptCipher(GenerateHash(password), ApplicationSystem.StringBuffer);
        }

        public static bool IsPasswordValidViaMethod1(string plainPassword, string hashedPassword)
        {
            return ValidateHash(DecryptCipher(hashedPassword, ApplicationSystem.StringBuffer), plainPassword);
        }

        public static bool IsPasswordInvalidViaMethod1(string plainPassword, string hashedPassword)
        {
            return !IsPasswordValidViaMethod1(plainPassword, hashedPassword);
        }

        public static string SecureTextViaMethod1(string plainText)
        {
            return EncryptCipher(plainText, ApplicationSystem.StringBuffer);
        }

        public static string UnsecureTextViaMethod1(string cipherText)
        {
            return DecryptCipher(cipherText, ApplicationSystem.StringBuffer);
        }
    }
}

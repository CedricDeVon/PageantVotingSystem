using PageantVotingSystem.Source.Configurations;

namespace PageantVotingSystem.Source.Security
{
    public class ApplicationCryptographer : Cryptographer
    {
        public static string SecurePassword(string password)
        {
            return EncryptCipher(GenerateHash(password), ApplicationConfiguration.EnvironmentValue("StringBuffer"));
        }

        public static bool ValidatePassword(string plainPassword, string hashedPassword)
        {
            return ValidateHash(DecryptCipher(hashedPassword, ApplicationConfiguration.EnvironmentValue("StringBuffer")), plainPassword);
        }
    }
}

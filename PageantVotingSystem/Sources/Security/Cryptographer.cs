
using System;
using System.Text;
using System.Security.Cryptography;

using Rijndael256;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace PageantVotingSystem.Sources.Security
{
    public class Cryptographer
    {
        public static string GenerateHash(string plainText)
        {
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] salt = new byte[8];
                RandomNumberGenerator.Create().GetBytes(salt);
                Argon2Config config = new Argon2Config
                {
                    Type = Argon2Type.DataIndependentAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = 1, // 3
                    MemoryCost = 1024, // 32768
                    Lanes = 4, // 5
                    Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                    Password = passwordBytes,
                    Salt = salt, // >= 8 bytes if not null
                    HashLength = 4 // >= 4
                };
                Argon2 argon2A = new Argon2(config);
                string hashString;
                using (SecureArray<byte> hashA = argon2A.Hash())
                {
                    hashString = config.EncodeString(hashA.Buffer);
                }
                return hashString;
            }
            catch
            {
                return "";
            }
        }

        public static bool ValidateHash(string hashText, string plainText)
        {
            try
            {
                return Argon2.Verify(hashText, Encoding.UTF8.GetBytes(plainText));
            }
            catch
            {
                return false;
            }
        }

        public static string EncryptCipher(string plainText, string password)
        {
            try
            {
                return Rijndael256.Rijndael.Encrypt(plainText, password, KeySize.Aes256);
            }
            catch
            {
                return "";
            }
        }

        public static string DecryptCipher(string cipherText, string password)
        {
            try
            {
                return Rijndael256.Rijndael.Decrypt(cipherText, password, KeySize.Aes256);
            }
            catch
            {
                return "";
            }
        }
    }
}

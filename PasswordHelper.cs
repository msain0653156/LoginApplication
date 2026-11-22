using System;
using System.Security.Cryptography;

namespace LoginApplication
{
    public static class PasswordHelper
    {
        public static byte[] CreateSalt()
        {
            byte[] salt = new byte[16];

            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(salt);

            return salt;
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, salt, 600000, HashAlgorithmName.SHA256))
                return hash.GetBytes(32);
        }

        public static bool VerifyPassword(string password, byte[] salt, byte[] savedHash)
        {
            byte[] enteredHash = HashPassword(password, salt);

            if (enteredHash.Length != savedHash.Length)
                return false;

            int difference = 0;

            for (int i = 0; i < enteredHash.Length; i++)
                difference |= enteredHash[i] ^ savedHash[i];

            return difference == 0;
        }
    }
}

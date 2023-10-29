using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.Domain.Security
{
    internal static class SecurityProvider
    {
        const int keySize = 64;
        const int iterations = 100;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromHexString(salt), iterations, hashAlgorithm, keySize);
            return Convert.ToHexString(hashToCompare) == hash;
        }
    }
}

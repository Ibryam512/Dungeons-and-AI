using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Helpers
{
    static class Hasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public static string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return $"{Convert.ToHexString(hash)}/{Convert.ToHexString(salt)}";
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var data = hash.Split('/');
            var salt = Convert.FromHexString(data[1]);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(data[0]));
        }
    }
}

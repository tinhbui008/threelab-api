using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Threelab.Application.Core.Abstractions;

namespace Threelab.Infrastructure.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int saltSize = 120 / 8;
        private const int keySize = 256 / 8;
        private const int iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        const char delimiter = ':';

        public string GeneratePassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, _hashAlgorithmName, keySize);


            return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string passwordHash, string inputPassword)
        {
            var elm = passwordHash.Split(delimiter);

            var salt = Convert.FromBase64String(elm[0]);
            var hash = Convert.FromBase64String(elm[1]);

            var hashInputPassword = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, iterations, _hashAlgorithmName, keySize);


            return CryptographicOperations.FixedTimeEquals(hash, hashInputPassword);
        }
    }
}

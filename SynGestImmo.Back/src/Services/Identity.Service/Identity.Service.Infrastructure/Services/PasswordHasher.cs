using Identity.Service.Application.Common;
using Identity.Service.Domain.Exceptions;
using System.Security.Cryptography;

namespace Identity.Service.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; //128bits
        private const int HashSize = 32; //256 bits
        private const int Iterations = 100000; // number of salting
        private const char Separator = ':';

        public string HashPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password)) 
                throw new IdentityServiceException("ERROR_HASH_PASSWORD","Password cannot be empty !");

            // generator
            using var rng = RandomNumberGenerator.Create();
            var salt = new Byte[SaltSize];
            rng.GetBytes(salt);

            // hashing password
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256
                );
            var hash = pbkdf2.GetBytes(HashSize);

            //formating
            return $"pbkdf2{Separator}{Iterations}{Separator}{Convert.ToBase64String(salt)}{Separator}{Convert.ToBase64String(hash)}";


        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            try
            {
                // split hashed password
                var parts = hashedPassword.Split(Separator);

                if (parts.Length != 4 || parts[0] != "pbkdf2")
                    return false;

                var iterations = int.Parse(parts[1]);
                var salt = Convert.FromBase64String(parts[2]);
                var storedHash = Convert.FromBase64String(parts[3]);

                // recalculate the hasing with the same values

                using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256
                );
                var computedHash = pbkdf2.GetBytes(storedHash.Length);

                //Constant-time comparison to avoid temporal attacks
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);

            }
            catch
            {
                return false;
            }

        }
    }
}

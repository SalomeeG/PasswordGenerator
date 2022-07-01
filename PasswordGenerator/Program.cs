using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            Console.WriteLine($"Hashed Password: {GeneratePasswordHashUsingSalt("pass", salt)}");
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;
            var length = 36;

            var hash = Rfc2898DeriveBytes.Pbkdf2(passwordText, salt, iterate, HashAlgorithmName.SHA1, length);

            return Convert.ToBase64String(hash);
        }
    }
}

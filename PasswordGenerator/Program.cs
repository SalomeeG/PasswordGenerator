using System;
using System.Security.Cryptography;

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

            GC.Collect();
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;
            var length = 36;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

            return Convert.ToBase64String(pbkdf2.GetBytes(length));
        }
    }
}

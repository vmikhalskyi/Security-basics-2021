using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace lab5._4
{ 
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] HashPasswordMD5(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.MD5))
            { 
                return rfc2898.GetBytes(16);
            }
        }

        public static byte[] HashPasswordSHA1(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA1))
            {
                return rfc2898.GetBytes(20);
            }
        }

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }

        public static byte[] HashPasswordSHA384(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA384))
            {
                return rfc2898.GetBytes(48);
            }
        }

        public static byte[] HashPasswordSHA512(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA512))
            {
                return rfc2898.GetBytes(64);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string passwordToHash = "JustPassword";

            HashPassword(passwordToHash, 180000);
            HashPassword(passwordToHash, 230000);
            HashPassword(passwordToHash, 280000);
            HashPassword(passwordToHash, 330000);
            HashPassword(passwordToHash, 380000);
            HashPassword(passwordToHash, 430000);
            HashPassword(passwordToHash, 480000);
            HashPassword(passwordToHash, 530000);
            HashPassword(passwordToHash, 580000);
            HashPassword(passwordToHash, 630000);
            Console.ReadLine();
        }

        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();

            var hashedPassword = PBKDF2.HashPasswordSHA256(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine("Password to hash: " + passwordToHash);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
        }
    }
}

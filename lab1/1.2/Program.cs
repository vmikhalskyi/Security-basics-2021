using System;
using System.Security.Cryptography;

namespace _1._2
{
    class Program
    {
        public static byte[] GenerateRandomNumber(int length) // новый метод public 
        {
            using (var rndNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                rndNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        static void Main(string[] args)
        {
            for(int i = 1; i < 10; i++)
            {
                string randomNumber = Convert.ToBase64String(GenerateRandomNumber(32));
                Console.WriteLine(randomNumber);
            }

        }
    }
}

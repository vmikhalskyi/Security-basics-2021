using System;
using System.Security.Cryptography;
using System.Text;

namespace lab5._5
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

        public static byte[] HashPasswordSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("For registration enter login and password.");
            Console.WriteLine("Enter login: ");
            string login = Convert.ToString(Console.ReadLine());
            byte[] salt = PBKDF2.GenerateSalt();
            Console.WriteLine("Enter password: ");
            string password = Convert.ToBase64String(PBKDF2.HashPasswordSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 180000));

            Console.WriteLine("Registration complete!");

            Console.WriteLine("To log in, please, enter your credentials:");
            Console.WriteLine("Enter login: ");
            string enteredLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string enteredPassword = Convert.ToBase64String(PBKDF2.HashPasswordSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine())), salt, 180000));

            if (login != enteredLogin)
            {
                Console.WriteLine("Entered login is incorrect!");
            }
            else if (password != enteredPassword)
            {
                Console.WriteLine("Entered password is incorrect!");
            }
            else
            {
                Console.WriteLine("Authorization complete!");
            }

        }
    }
}

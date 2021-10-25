using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3_4
{
    class Program
    {

        static byte[] ComputeHashSHA256(byte[] dataForHash)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(dataForHash);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("For registration enter login and password.");
            Console.WriteLine("Enter login: ");
            string login = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string password = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));

            Console.WriteLine("Registration complete!");

            Console.WriteLine("To log in, please, enter your credentials:");
            Console.WriteLine("Enter login: ");
            string enteredLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            string enteredPassword = Convert.ToBase64String(ComputeHashSHA256(Encoding.Unicode.GetBytes(Convert.ToString(Console.ReadLine()))));

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


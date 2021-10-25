﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3_3
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

        public static byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

        static void Main(string[] args)
        {
            string key = "Vladyslav123";
            string message = "Today is sunny day";

            var hashedKey = ComputeHashSHA256(Encoding.Unicode.GetBytes(key));

            var hashedMess = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), hashedKey);

            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Hashed message: {Convert.ToBase64String(hashedMess)}");

            Console.WriteLine("Sending message to receiver....");

            string key2 = "Vladyslav123";

            var hashedKey2 = ComputeHashSHA256(Encoding.Unicode.GetBytes(key2));

            var hashedMess2 = ComputeHmacsha256(Encoding.Unicode.GetBytes(message), hashedKey2);

            if (Convert.ToBase64String(hashedMess) == Convert.ToBase64String(hashedMess2))
            {
                Console.WriteLine("Message is correct!");
            }
            else
            {
                Console.WriteLine("Message is broken!");
            }
        }
    }
}

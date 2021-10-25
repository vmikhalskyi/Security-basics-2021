using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3_2
{
    class Program
    {
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }

        static void Main(string[] args)
        {
            string hash = "po1MVkAE7IjUUwu61XxgNg==";

            for(int i = 100000000; i <= 199999999; i++)
            {
                var receivedHash = Convert.ToBase64String(ComputeHashMd5(Encoding.Unicode.GetBytes(i.ToString().Substring(1, 8))));

                if (receivedHash == hash)
                {
                    Console.WriteLine($"User's password is {i.ToString().Substring(1, 8)}");
                }
            }
        }
    }
}

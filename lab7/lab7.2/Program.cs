﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7._2
{
    class RSAWithRSAParameterKey
    {
        private RSAParameters _privateKey;

        public void AssignNewKeys(string publicKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cypherbytes;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                cypherbytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cypherbytes;
        }

        public byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainText;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plainText = rsa.Decrypt(dataToDecrypt, true);
            }

            return plainText;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var rsaParams = new RSAWithRSAParameterKey();

            Console.WriteLine("Enter text to encrypt: ");
            string original = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter desired path to public key that will be generated: ");
            string publicKeyPath = Convert.ToString(Console.ReadLine());
            rsaParams.AssignNewKeys(publicKeyPath);

            Console.WriteLine("Original Text: " + original);

            var encrypted = rsaParams.EncryptData(publicKeyPath, Encoding.Unicode.GetBytes(original));
            Console.WriteLine("Encrypted Text: " + Convert.ToBase64String(encrypted));

            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine("Decrypted Text: " + Encoding.Default.GetString(decrypted));

            Console.WriteLine("Want to encrypt text with someone's public key? y/n: ");
            string encryptAnother = Convert.ToString(Console.ReadLine());

            if (encryptAnother == "y")
            {
                Console.WriteLine("Enter text to encrypt: ");
                string additionalOriginal = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter path to existed public key: ");
                string additionalPublicKeyPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Original Text: " + additionalOriginal);
                var additionalEncrypted = rsaParams.EncryptData(additionalPublicKeyPath, Encoding.Unicode.GetBytes(additionalOriginal));
                Console.WriteLine("Encrypted Text: " + Convert.ToBase64String(additionalEncrypted));
            }
        }
    }
}

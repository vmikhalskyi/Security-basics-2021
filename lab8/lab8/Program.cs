using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lab7._2
{
    class RSAWithRSAParameterKey
    {
        public void AssignNewKey(string publicKeyPath, string
            privateKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
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

        public byte[] DecryptData(string privateKeyPath, byte[] dataToDecrypt)
        {
            byte[] plainText;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
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

            Console.WriteLine("Choose what you want to do: generate new public and private keys, encrypt message or decrypt message (g/e/d):");
            string action = Convert.ToString(Console.ReadLine());

            if (action == "g")
            {
                Console.WriteLine("Enter desired path to newly generated public key: ");
                string publicKeyPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter desired path to newly generated private key: ");
                string privateKeyPath = Convert.ToString(Console.ReadLine());

                rsaParams.AssignNewKey(publicKeyPath, privateKeyPath);
                Console.WriteLine("Done! Check your newly generated keys in folder");

            } else if (action == "e")
            {
                Console.WriteLine("Enter path to public key which we will use to encrypt data: ");
                string publicKeyPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter message to encrypt: ");
                string message = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter desired path to encrypted message (.dat format):");
                string messagePath = Convert.ToString(Console.ReadLine());

                var encData = rsaParams.EncryptData(publicKeyPath, Encoding.Unicode.GetBytes(message));
                Console.WriteLine("Encrypted Text: " + Convert.ToBase64String(encData));


                File.WriteAllBytes(messagePath, encData);
                Console.WriteLine("Encrypted message was saved");

            } else if (action == "d")
            {
                Console.WriteLine("Enter path to private key which we will use to decrypt data: ");
                string privateKeyPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter path to message to decrypt: ");
                string messagePath = Convert.ToString(Console.ReadLine());

                byte[] newEncData = File.ReadAllBytes(messagePath).ToArray();

                var decrypted = rsaParams.DecryptData(privateKeyPath, newEncData);
                Console.WriteLine("Decrypted Text: " + Encoding.Default.GetString(decrypted));

            } else
            {
                Console.WriteLine("Invalid command, try again");
            }
        }
    }
}
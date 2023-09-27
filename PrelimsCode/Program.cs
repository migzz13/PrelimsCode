using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrelimsCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Would you like to encrypt or decrypt a message? [E / D] : ");
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "e":
                    Console.Write("Enter the encryption key (phrase or sentence): ");
                    string encryptionKey = Console.ReadLine();
                    Console.Write("Enter the message to encrypt: ");
                    string messageToEncrypt = Console.ReadLine();

                    EncryptMessage(encryptionKey, messageToEncrypt);
                    Console.ReadKey();
                    break;

                case "d":
                    Console.Write("Enter the decryption key (phrase or sentence): ");
                    string decryptionKey = Console.ReadLine();

                    DecryptMessage(decryptionKey);
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Invalid setting. Please try again. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }

        static void EncryptMessage(string key, string message)
        {
            string encryptedMessage = Encrypt(message, key);

            SaveToFile("encrypted_message.txt", encryptedMessage);

            Console.WriteLine($"Encrypted message: {encryptedMessage}");
        }

        static void DecryptMessage(string key)
        {
            string encryptedMessageFromFile = ReadFromFile("encrypted_message.txt");

            string decryptedMessage = Decrypt(encryptedMessageFromFile, key);
            Console.WriteLine($"Decrypted message: {decryptedMessage}");
        }

        static void SaveToFile(string fileName, string content)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(content);
            }
        }

        static string ReadFromFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        static string Encrypt(string input, string key)
        {
            char[] result = input.ToCharArray();
            int keyLength = key.Length;

            for (int i = 0; i < result.Length; i++)
            {
                if (char.IsLetter(result[i]))
                {
                    char offset = char.IsUpper(result[i]) ? 'A' : 'a';
                    int keyIndex = i % keyLength;
                    int keyOffset = char.IsUpper(key[keyIndex]) ? 'A' : 'a';
                    int shift = key[keyIndex] - keyOffset;

                    result[i] = (char)((result[i] + shift - offset + 26) % 26 + offset);
                }
            }

            return new string(result);
        }

        static string Decrypt(string input, string key)
        {
            char[] result = input.ToCharArray();
            int keyLength = key.Length;

            for (int i = 0; i < result.Length; i++)
            {
                if (char.IsLetter(result[i]))
                {
                    char offset = char.IsUpper(result[i]) ? 'A' : 'a';
                    int keyIndex = i % keyLength;
                    int keyOffset = char.IsUpper(key[keyIndex]) ? 'A' : 'a';
                    int shift = key[keyIndex] - keyOffset;

                    int newPosition = (result[i] - offset - shift + 26) % 26;
                    result[i] = (char)(newPosition + offset);
                }
            }

            return new string(result);
        }
    }
}

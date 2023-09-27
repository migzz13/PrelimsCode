﻿using System;
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
            string choice = Console.ReadLine().ToUpper();
            Console.WriteLine("Machine Mode has been set.");
            Console.ReadKey();

            switch (choice)
            {
                case "E":
                    Console.Clear();
                    Console.Write("What is the key you want to set? : ");
                    string encryptionKey = Console.ReadLine();
                    Console.WriteLine("Cypher has been set.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Please enter the message you want to encrypt: ");
                    string messageToEncrypt = Console.ReadLine();

                    EncryptMessage(encryptionKey, messageToEncrypt);
                    Console.WriteLine("Press any key to close the program");
                    Console.ReadKey();
                    break;

                case "D":
                    Console.Clear();
                    Console.Write("Please enter the key you want to set? : ");
                    string decryptionKey = Console.ReadLine();
                    Console.WriteLine("Cypher has been set");
                    Console.ReadKey();
                    Console.Clear();
                    DecryptMessage(decryptionKey);
                    Console.WriteLine("Message has been successfully decrypted.");
                    Console.WriteLine("Press any key to close the program");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Invalid Setting please try again. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }

        static void EncryptMessage(string key, string message)
        {
            string encryptedMessage = Encrypt(message, key);

            SaveToFile("eMessage.txt", encryptedMessage);

            Console.WriteLine("Message has been successfully encrypted and written to eMessage.txt");
        }

        static void DecryptMessage(string key)
        {
            string encryptedMessageFromFile = ReadFromFile("eMessage.txt");

            string decryptedMessage = Decrypt(encryptedMessageFromFile, key);
            Console.WriteLine("Reading eMessage.txt and decrypting using the provided key.");
            Console.WriteLine($"The decrypted message is: ");
            Console.WriteLine(decryptedMessage);
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

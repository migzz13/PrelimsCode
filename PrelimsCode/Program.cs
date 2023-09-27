using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrelimsCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Would you like to encrypt or decrypt a message? [E / D] : ");
            
            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "e":
                    Console.Write("Enter the encryption key (an integer): ");
                    int encryptionKey = int.Parse(Console.ReadLine());
                    Console.Write("Enter the message to encrypt: ");
                    string messageToEncrypt = Console.ReadLine();

                    string encryptedMessage = Encrypt(messageToEncrypt, encryptionKey);
                    Console.WriteLine($"Encrypted message: {encryptedMessage}");
                    break;

                case "d":
                    Console.Write("Enter the decryption key (an integer): ");
                    int decryptionKey = int.Parse(Console.ReadLine());
                    Console.Write("Enter the message to decrypt: ");
                    string messageToDecrypt = Console.ReadLine();

                    string decryptedMessage = Decrypt(messageToDecrypt, decryptionKey);
                    Console.WriteLine($"Decrypted message: {decryptedMessage}");
                    break;

                default:
                    Console.WriteLine("Invalid Setting please try again. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }

        static string Encrypt(string input, int key)
        {
            char[] result = input.ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                if (char.IsLetter(result[i]))
                {
                    char offset = char.IsUpper(result[i]) ? 'A' : 'a';
                    result[i] = (char)((result[i] + key - offset) % 26 + offset);
                }
            }

            return new string(result);
        }
        static string Decrypt(string input, int key)
        {
            char[] result = input.ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                if (char.IsLetter(result[i]))
                {
                    char offset = char.IsUpper(result[i]) ? 'A' : 'a';
                    int newPosition = (result[i] - offset - key + 26) % 26;
                    result[i] = (char)(newPosition + offset);
                }
            }

            return new string(result);
        }
    }
}

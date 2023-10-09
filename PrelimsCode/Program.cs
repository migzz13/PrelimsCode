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
        static List<char> alphabet = new List<char>();
        static char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 
            'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 
            'X', 'Y', 'Z' };
        static void Main(string[] args)
        {
            MainDisplay();
        }

        static void MainDisplay()
        {
            bool cipher = false;

            while (!cipher)
            {
                Console.Write("Would you like to encrypt or decrypt a message? [E / D] : ");
                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "E":
                        Console.WriteLine("Machine Mode has been set.");
                        Console.ReadKey();
                        Console.Clear();

                        string key = Key();
                        alphabet = ModifiedAlphabet(letters, key);
                        Console.WriteLine("Please input the message you want to encrypt:");
                        string word = Console.ReadLine().ToUpper();
                        string final = Encryption(word, letters, alphabet);
                        StreamWriter(final, "eMessage.txt");
                        Console.ReadKey();
                        cipher = true;
                        break;

                    case "D":
                        Console.WriteLine("Machine Mode has been set.");
                        Console.ReadKey();
                        Console.Clear();

                        key = Key();
                        alphabet = ModifiedAlphabet(letters, key);
                        word = StreamReader("eMessage.txt");
                        final = Decryption(word, letters, alphabet);
                        Console.WriteLine("Reading eMessage.txt and decrypting using the provided key.");
                        Console.WriteLine("The decrypted message is:");
                        Console.WriteLine(final);
                        Console.WriteLine("Message has been successfully decrypted.");
                        Console.WriteLine("Press any key to close the program");
                        Console.ReadKey();
                        cipher = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Setting please try again. Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        static string Key()
        {
            string key = "";
            Console.Write("What is the key you want to set? : ");
            key = Console.ReadLine().ToUpper();
            Console.WriteLine("Cypher has been set.");
            Console.ReadKey();
            Console.Clear();
            return key;
        }

        static List<char> ModifiedAlphabet(char[] letter, string key)
        {
            List<char> temp = new List<char>();
            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];
                int cint = (int)c;
                if (cint > 64 && cint < 91)
                {
                    if (!temp.Contains(c))
                    {
                        temp.Add(c);
                    }
                }
            }
            for (int i = 0; i < letters.Length; i++)
            {
                if (!temp.Contains(letters[i]))
                {
                    temp.Add(letters[i]);
                }
            }
            return temp;
        }

        static string Encryption(string message, char[] letters, List<char> encryptletters)
        {
            string encryptedMessage = "";
            foreach (char letter in message)
            {
                if (char.IsUpper(letter))
                {
                    for (int i = 0; i < letters.Length; i++)
                    {
                        if (letter == letters[i])
                        {
                            encryptedMessage += encryptletters[i];
                            break;
                        }
                    }
                }
                else
                {
                    encryptedMessage += letter;
                }
            }

            return encryptedMessage;
        }

        static string Decryption(string encryptMessage, char[] letters, List<char> encryptletters)
        {
            string decryptedMessage = "";

            foreach (char letter in encryptMessage)
            {
                if (char.IsUpper(letter))
                {
                    for (int i = 0; i < encryptletters.Count; i++)
                    {
                        if (letter == encryptletters[i])
                        {
                            decryptedMessage += letters[i];
                            break;
                        }
                    }
                }
                else
                {
                    decryptedMessage += letter;
                }
            }

            return decryptedMessage;
        }

        static void StreamWriter(string final, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(final);
            }
            Console.WriteLine("Message has been successfully encrypted and written to eMessage.txt");
            Console.WriteLine("Press any key to close the program");
        }

        static string StreamReader(string fileName)
        {
            string total = "";
            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    total += line;
                }
            }
            return total;
        }
    }
}

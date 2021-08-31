using System;
using static System.IO.File;

namespace ltx_tool
{
    class LtxTool
    {
        static void Main(string[] args)
        {
            if (!BitConverter.IsLittleEndian)
            {
                Console.WriteLine("ERROR: Computer architecture not little endian!");
                Console.WriteLine("\nPress any key to close.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            string textFilePath;
            if (args.Length == 0)
            {
                textFilePath = GetUserInput();
            }
            else
            {
                textFilePath = args[args.Length - 1];
            }

            byte[] bytes = new byte[1];
            if (Exists(textFilePath))
            {
                bytes = ReadAllBytes(textFilePath);
            } else
            {
                Console.WriteLine("ERROR: File does not exist! Please double check given path.");
                Console.WriteLine("\nPress any key to close.");
                Console.ReadKey();
                Environment.Exit(2);
            }

            string magicNumber = GetString(bytes);
            Console.WriteLine(magicNumber);

            float version = BitConverter.ToSingle(bytes, 8);
            Console.WriteLine("version: " + version);

            int textEntries = BitConverter.ToInt32(bytes, 12);
            Console.WriteLine("textEntries: " + textEntries);

            int pointer = 16;
            for (int i = 0; i < textEntries; i++)
            {
                byte unknownByte = bytes[pointer];
                Console.WriteLine("\nunknownByte: " + unknownByte);
                pointer += 1;

                int unknown = BitConverter.ToInt32(bytes, pointer);
                Console.WriteLine("unknown: " + unknown);
                pointer += 4;

                int charsInFollowingString = BitConverter.ToInt32(bytes, pointer);
                Console.WriteLine("charsInFollowingString: " + charsInFollowingString);
                pointer += 4;

                byte[] dialogue = new byte[charsInFollowingString * 2];
                Array.Copy(bytes, pointer, dialogue, 0, charsInFollowingString * 2);
                Console.WriteLine(GetUnicode(dialogue));
                pointer += charsInFollowingString * 2;
            }

            Console.WriteLine("\nPress any key to close.");
            Console.ReadKey();
        }

        private static string GetUserInput()
        {
            Console.WriteLine("Please provide the path of the archive to unpack");
            Console.Write("-> ");
            return Console.ReadLine();
        }

        private static string GetArchiveName(string path)
        {
            string name;
            if (path.Contains("\\"))
            {
                string[] splitPath = path.Split('\\');
                name = splitPath[splitPath.Length - 1];
            }
            else
            {
                name = path;
            }
            return name;
        }

        private static string GetString(byte[] bytes)
        {
            string newString = "";
            for (int i = 0; i < 8; i++)
            {
                newString += (char)bytes[i];
            }
            return newString;
        }

        private static string GetUnicode(byte[] bytes)
        {
            string newString = "";
            for (int i = 0; i < bytes.Length; i += 2)
            {
                char character = BitConverter.ToChar(bytes, i);
                newString += character;
            }
            return newString;
        }
    }
}

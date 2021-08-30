using System;
using static System.IO.File;

namespace ltx_tool
{
    class LtxTool
    {
        static void Main(string[] args)
        {
            byte[] bytes = ReadAllBytes("C:\\Users\\etste\\source\\repos\\ltx-tool\\testData\\Aragorn.ltx");

            // Header
            Console.WriteLine("Is little Endian? -> " + BitConverter.IsLittleEndian);
            string magicNumber = getString(bytes);
            Console.WriteLine(magicNumber);

            float version = BitConverter.ToSingle(bytes, 8);
            Console.WriteLine("version: " + version);

            int textEntries = BitConverter.ToInt32(bytes, 12);
            Console.WriteLine("textEntries: " + textEntries); // Text entry count?

            // D1
            byte unknownByte = bytes[16];
            Console.WriteLine("\nunknownByte: " + unknownByte);

            int unknown = BitConverter.ToInt32(bytes, 17);
            Console.WriteLine("unknown: " + unknown);

            int charsInFollowingString = BitConverter.ToInt32(bytes, 21);
            Console.WriteLine("charsInFollowingString: " + charsInFollowingString);

            byte[] dialogue1 = new byte[charsInFollowingString * 2];
            Array.Copy(bytes, 25, dialogue1, 0, charsInFollowingString * 2);
            Console.WriteLine(getUnicode(dialogue1));

            // D2
            byte unknownByte2 = bytes[87];
            Console.WriteLine("\nunknownByte: " + unknownByte2);

            int unknown2 = BitConverter.ToInt32(bytes, 88);
            Console.WriteLine("unknown: " + unknown2);

            int chars2 = BitConverter.ToInt32(bytes, 92);
            Console.WriteLine("charsInFollowingString: " + chars2);

            byte[] dialogue2 = new byte[chars2 * 2];
            Array.Copy(bytes, 96, dialogue2, 0, chars2 * 2);
            Console.WriteLine(getUnicode(dialogue2));

            // D3
            byte unknownByte3 = bytes[144];
            Console.WriteLine("\nunknownByte: " + unknownByte3);

            int unknown3 = BitConverter.ToInt32(bytes, 145);
            Console.WriteLine("unknown: " + unknown3);

            int chars3 = BitConverter.ToInt32(bytes, 149);
            Console.WriteLine("charsInFollowingString: " + chars3);

            byte[] dialogue3 = new byte[chars3 * 2];
            Array.Copy(bytes, 153, dialogue3, 0, chars3 * 2);
            Console.WriteLine(getUnicode(dialogue3));

            Console.ReadKey();
        }

        private static string getString(byte[] bytes)
        {
            string newString = "";
            for (int i = 0; i < 8; i++)
            {
                newString += (char)bytes[i];
            }
            return newString;
        }

        private static string getUnicode(byte[] bytes, int start, int to)
        {
            string newString = "";
            for (int i = start; i < to; i += 2)
            {
                char character = BitConverter.ToChar(bytes, i);
                newString += character;
            }
            return newString;
        }

        private static string getUnicode(byte[] bytes)
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

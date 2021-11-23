using System;
using System.Collections.Generic;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr;
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }
            try
            {
                sr = new StreamReader(args[0]);
            }
            catch (Exception)
            {
                Console.WriteLine("File Error");
                return;
            }
            Dictionary<byte, ulong> frequencies = ReadInput(sr);
            if (frequencies.Count == 0) return;

            Huffman h = new Huffman();

            List<Node> tree = h.BuildTree(frequencies);
            Node root = h.BuildHuffmanTree(ref tree);

            Huffman.PrintTree(root);

            h.GetCodingFromHuffmanTree(root, "");
            GetHexFromBin("10001001");
            StreamReader sr2;
            Encode(sr2 = new StreamReader(args[0]), h.Encodings);
            
            Console.ReadKey();
        }

        static Dictionary<byte, ulong> ReadInput(StreamReader sr)
        {
            Dictionary<byte, ulong> frequencies = new Dictionary<byte, ulong>();
            using (sr)
            {
                int b = sr.Read();
                while (b != -1)
                {
                    if (IsAscii((char)b))
                    {
                        byte key = Convert.ToByte(b);
                        if (frequencies.ContainsKey(key))
                        {
                            frequencies[key]++;
                        }
                        else
                        {
                            frequencies.Add(key, 1);
                        }
                    }

                    b = sr.Read();
                }
            }
            return frequencies;
        }
        static void Encode(StreamReader sr, Dictionary<byte, string> Encodings)
        {
            string encodedByte = "";
            using (sr)
            {
                int c = sr.Read();
                while (c != -1)
                {
                    if (IsAscii((char)c))
                    {
                        byte key = Convert.ToByte(c);
                        encodedByte += Encodings[key];

                        if (encodedByte.Length >= 8)
                        {
                            GetHexFromBin(encodedByte);
                            encodedByte = encodedByte.Remove(0, 8);
                        }
                    }
                    c = sr.Read();
                }
                // if the last byte is not complete, add zeros to the end.
                if (encodedByte.Length != 0)
                {
                    int threshold = 8 - encodedByte.Length;
                    for (int i = 0; i < threshold; i++)
                    {
                        encodedByte += "0";
                    }
                }
                GetHexFromBin(encodedByte);
            }
        }
        private static void GetHexFromBin(string binary)
        {
            binary = Reverse(binary);
            int i = Convert.ToInt32(binary, 2);
            string result = i.ToString("X2");
            Console.WriteLine("0x" + result); 
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static void Main2(string[] args)
        {
            FileStream fs;
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }
            try
            {
                fs = File.OpenRead(Path.GetFullPath(args[0]));
            }
            catch (Exception)
            {
                Console.WriteLine("File Error");
                return;
            }
            Dictionary<byte, ulong> frequencies = ReadInput2(fs);
            if (frequencies.Count == 0) return;
            //List<Node> tree = Huffman.BuildTree(frequencies);
            //Node root = Huffman.BuildHuffmanTree(ref tree);
            //Huffman.PrintTree(root);


        }
        static Dictionary<byte, ulong> ReadInput2(FileStream fs)
        {
            Dictionary<byte, ulong> frequencies = new Dictionary<byte, ulong>();
            using (fs)
            {
                int b = fs.ReadByte();
                while (b != -1)
                {
                    if (IsAscii((char)b))
                    {
                        byte key = Convert.ToByte(b);
                        if (frequencies.ContainsKey(key))
                        {
                            frequencies[key]++;
                        }
                        else
                        {
                            frequencies.Add(key, 1);
                        }
                    }
                    b = fs.ReadByte();
                }
            }
            return frequencies;
        }
        public static bool IsAscii(char c)
        {
            return (c <= byte.MaxValue);
        }
    }
}

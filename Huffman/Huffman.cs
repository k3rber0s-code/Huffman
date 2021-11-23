using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Huffman
    {
        public Dictionary<byte, string> Encodings;
        public Huffman()
        {
            Encodings = new Dictionary<byte, string>();
        }
        public List<Node> BuildTree (Dictionary<byte, ulong> Frequencies)
        {
            List<Node> Tree = new List<Node>();
            foreach(var f in Frequencies)
            {
                Tree.Add(new Node(f.Key, f.Value, true)); // ALL INITAL VALUES ARE LEAVES
            }
            return Tree;
        }
        private static Node GetMinNodeFromTree (ref List<Node> Tree)
        {
            Node minNode = new Node(byte.MaxValue, ulong.MaxValue, false);
            foreach (Node n in Tree)
            {
                if (minNode.Weight > n.Weight)
                {
                    minNode = n;
                }
                else if (n.Weight == minNode.Weight &&
                    n.IsLeaf == true && minNode.IsLeaf == false)
                {
                    minNode = n;
                }
                else if (n.Weight == minNode.Weight &&
                    n.IsLeaf == minNode.IsLeaf &&
                    n.Value < minNode.Value)
                {
                    minNode = n;
                }
            }
            Tree.Remove(minNode);
            return minNode;
        }
        public Node BuildHuffmanTree(ref List<Node> Tree)
        {
            if (Tree.Count == 1 )
            {
                return Tree[0];
            }
            else
            {
                Node n1 = GetMinNodeFromTree(ref Tree);
                Node n2 = GetMinNodeFromTree(ref Tree);
                Node parent = new Node(0, n1.Weight + n2.Weight, false);

                if (n1.Weight == n2.Weight)
                {
                    // LEAF IS LIGHTER THAT INTERNAL NODE
                    if (n1.IsLeaf != n2.IsLeaf)
                    {
                        if (n1.IsLeaf)
                        {
                            parent.LeftChild = n1;
                            parent.RightChild = n2;
                        }
                        else
                        {
                            parent.LeftChild = n2;
                            parent.RightChild = n1;
                        }
                    }
                    
                    else
                    {
                        // ASCII VALUE OF LEAVES DETERMINE THE PRIOTITY
                        if (n1.IsLeaf == true && n2.IsLeaf == true)
                        {
                            if (n1.Value < n2.Value)
                            {
                                parent.LeftChild = n1;
                                parent.RightChild = n2;
                            }
                            else
                            {
                                parent.LeftChild = n2;
                                parent.RightChild = n1;
                            }
                        }
                        else
                        {
                            // CREATION TIME OF INTERNAL NODES IS SOLVED AUTOMATICALLY BY LIST STRUCTURE
                            parent.LeftChild = n1;
                            parent.RightChild = n2;
                        }
                    }
                }
                else
                {
                    parent.LeftChild = n1;
                    parent.RightChild = n2;
                }
                Tree.Add(parent);
                return BuildHuffmanTree(ref Tree);
            }
        }
        public static void PrintTree(Node root)
        {
            if (root.IsLeaf)
            {
                Console.Write(" *{0}:{1}", (int)root.Value, root.Weight);
            }
            else
            {
                Console.Write(" {0}", root.Weight);
                PrintTree(root.LeftChild);
                PrintTree(root.RightChild);
            }
        }
        public string FormatNode(Node node)
        {
            string result = "";
            if (node.IsLeaf)
            {
                result += "1";
            }
            else
            {
                result += "0";
            }
            return "hello";

        }
        public void GetCodingFromHuffmanTree(Node startNode, string bitStream)
        {
            if (startNode.IsLeaf)
            {
                Encodings.Add(startNode.Value, bitStream);
            }
            else
            {
                if (startNode.LeftChild != null)
                {
                    GetCodingFromHuffmanTree(startNode.LeftChild, bitStream + "0");
                }
                if (startNode.RightChild != null)
                {
                    GetCodingFromHuffmanTree(startNode.RightChild, bitStream + "1");
                }
            }
        }
    }
}

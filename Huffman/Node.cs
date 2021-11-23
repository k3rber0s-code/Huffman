using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Node
    {
        public byte Value { get; set; }
        public ulong Weight { get; set; }
        public bool IsLeaf { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node(byte value, ulong weight, bool isLeaf)
        {
            Value = value;
            Weight = weight;
            IsLeaf = isLeaf;
            LeftChild = null;
            RightChild = null;
        }

    }
}

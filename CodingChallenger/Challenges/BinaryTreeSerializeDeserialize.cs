using CodingChallenger.Framework;
using CodingChallenger.GenericDataStructures.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Basically an open bracket represents children for that node before, comma separates between left and right child, and close bracket represents the end of the children string.
    /// The root however is exposed.
    /// An example output is 1(2(4,),3(5,6(,7)))
    /// For desterilizing though, a few tricks have to be made to tune down the run time due to string operations.
    /// Rather than using substring, I used an index keeping track of the position of the string and advance through the string for whichever symbol.
    /// </summary>
    [Challenge(Challenge.Done)]
    class BinaryTreeSerializeDeserialize : IChallenge<ChallengeTreeNode, ChallengeTreeNode> {
        public ChallengeTreeNode ExpectedOutput() {
            var tree = new ChallengeTreeNode(1);
            tree.left = new ChallengeTreeNode(2);
            tree.right = new ChallengeTreeNode(3);
            tree.left.left = new ChallengeTreeNode(4);
            tree.right.left = new ChallengeTreeNode(5);
            tree.right.right = new ChallengeTreeNode(6);
            tree.right.right.right = new ChallengeTreeNode(7);
            return tree;
        }

        public ChallengeTreeNode Input() {
            var tree = new ChallengeTreeNode(1);
            tree.left = new ChallengeTreeNode(2);
            tree.right = new ChallengeTreeNode(3);
            tree.left.left = new ChallengeTreeNode(4);
            tree.right.left = new ChallengeTreeNode(5);
            tree.right.right = new ChallengeTreeNode(6);
            tree.right.right.right = new ChallengeTreeNode(7);
            return tree;
        }

        public ChallengeTreeNode Run(ChallengeTreeNode input) {
            var codec = new Codec();
            var result = codec.serialize(input);
            var data = codec.deserialize(result);
            return data;
        }
    }

    class Codec {

        // Encodes a tree to a single string.
        public string serialize(ChallengeTreeNode root) {
            if (root == null) {
                return "";
            }

            var builder = new StringBuilder();
            GetString(root, builder);

            return builder.ToString();
        }

        // Decodes your encoded data to tree.
        public ChallengeTreeNode deserialize(string data) {
            if (string.IsNullOrEmpty(data)) {
                return null;
            }

            var rootVal = GetNextSymbol(data, 0);
            var root = new ChallengeTreeNode(int.Parse(rootVal));
            var index = AdvanceString(0, rootVal);

            if (index == data.Length) {
                return root;
            }

            GetTree(root, data, index);

            return root;
        }

        private int GetTree(ChallengeTreeNode parent, string data, int index) {
            //this function should always be called on an opening backet
            index = AdvanceString(index, "(");
            var nextSymbol = GetNextSymbol(data, index);
            if (nextSymbol != ",") {
                parent.left = new ChallengeTreeNode(int.Parse(nextSymbol));
                index = AdvanceString(index, nextSymbol);
                if (GetNextSymbol(data, index) == "(") {
                    index = GetTree(parent.left, data, index);
                }
            }

            //here, we should find a comma
            index = AdvanceString(index, ",");
            nextSymbol = GetNextSymbol(data, index);
            if (nextSymbol != ")") {
                parent.right = new ChallengeTreeNode(int.Parse(nextSymbol));
                index = AdvanceString(index, nextSymbol);
                if (GetNextSymbol(data, index) == "(") {
                    index = GetTree(parent.right, data, index);
                }
            }

            //here, we should find a right bracket
            index = AdvanceString(index, ")");

            //last thing, return our data string
            return index;
        }

        private void GetString(ChallengeTreeNode node, StringBuilder builder) {

            builder.Append(node.val);
            if (node.left == null && node.right == null) {
                return;
            }
            builder.Append('(');
            if (node.left != null) {
                GetString(node.left, builder);
            }
            builder.Append(',');
            if (node.right != null) {
                GetString(node.right, builder);
            }
            builder.Append(')');
        }

        private string GetNextSymbol(string s, int index) {
            if (s[index] == '(') {
                return "(";
            } else if (s[index] == ',') {
                return ",";
            } else if (s[index] == ')') {
                return ")";
            } else {
                var num = "";
                for (var i = index; i < s.Length; i++) {
                    if (char.IsDigit(s[i]) || s[i] == '-') {
                        num = num + s[i];
                    } else {
                        return num;
                    }
                }
                if (string.IsNullOrEmpty(num)) {
                    throw new ArgumentException($"Unable to parse string: {s}");
                } else {
                    return num;
                }
            }
        }

        private int AdvanceString(int index, string advance) {
            return index + advance.Length;
        }
    }
}

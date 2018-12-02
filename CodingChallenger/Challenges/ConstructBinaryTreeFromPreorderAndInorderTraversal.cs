using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using CodingChallenger.GenericDataStructures.Tree;
using CodingChallenger.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// basically, the frst value of the preorder is the root, and the inorder will bisect it between the left and right subtree
    /// This can then be recurisvely be done to construct the tree
    /// </summary>
    [Challenge(Challenge.Done)]
    class ConstructBinaryTreeFromPreorderAndInorderTraversal : IChallenge<Tuple<int[], int[]>, ChallengeTreeNode> {
        public ChallengeTreeNode ExpectedOutput() {
            return TreeMaker.MakeTreeFromString("3, 9, 20, null, null, 15, 7");
        }

        public Tuple<int[], int[]> Input() {
            var preorder = new int[] { 3, 9, 20, 15, 7 };
            var inorder = new int[] { 9, 3, 15, 20, 7 };
            return new Tuple<int[], int[]>(preorder, inorder);
        }

        public ChallengeTreeNode Run(Tuple<int[], int[]> input) {
            var preorder = input.Item1;
            var inorder = input.Item2;

            if (preorder.Length == 0 || inorder.Length == 0) {
                return null;
            }

            // turn the preorder into a stack
            var preorderStack = new Stack<int>(preorder.Reverse());
            // turn inorder into a list
            var inorderList = inorder.ToList();

            var root = ConstructNode(preorderStack, inorderList);

            return root;
        }

        private ChallengeTreeNode ConstructNode(Stack<int> preorderStack, List<int> inorderList) {
            var nodeVal = preorderStack.Pop();
            var valInorderIndex = inorderList.IndexOf(nodeVal);
            var inorderLeft = inorderList.GetRange(0, valInorderIndex);
            var inorderRight = inorderList.GetRange(valInorderIndex + 1, inorderList.Count - valInorderIndex - 1);

            var node = new ChallengeTreeNode(nodeVal);

            if (inorderLeft.Count != 0) {
                node.left = ConstructNode(preorderStack, inorderLeft);
            }
            if (inorderRight.Count != 0) {
                node.right = ConstructNode(preorderStack, inorderRight);
            }

            return node;
        }
    }
}

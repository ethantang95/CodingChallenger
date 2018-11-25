using CodingChallenger.Framework;
using CodingChallenger.GenericDataStructures.Tree;
using CodingChallenger.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.DoNotRun)]
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

            return null;
        }
    }
}

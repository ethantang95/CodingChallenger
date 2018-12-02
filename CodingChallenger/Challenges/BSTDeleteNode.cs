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
    /// There are a lot of corner cases to consider... a lot of corner cases
    /// Best to think of it from a use case scenario and come up with all the possible cases of deleting a node
    /// </summary>
    [Challenge(Challenge.Done)]
    class BSTDeleteNode : IChallenge<NodeAndVal, ChallengeTreeNode> {
        public ChallengeTreeNode ExpectedOutput() {
            var root = TreeMaker.MakeTreeFromString("27,17,33,15,26,32,45,7,16,23,null,31,null,40,48,5,14,null,null,21,25,29,null,34,41,46,null,2,6,10,null,20,22,24,null,28,30,null,39,null,42,null,47,1,4,null,null,9,11,18,null,null,null,null,null,null,null,null,null,37,null,null,44,null,null,0,null,3,null,8,null,null,13,null,19,35,38,43,null,null,null,null,null,null,null,12,null,null,null,null,36");
            return root;
        }

        public NodeAndVal Input() {
            var root = TreeMaker.MakeTreeFromString("27,17,33,15,26,32,45,7,16,23,null,31,null,40,49,5,14,null,null,21,25,29,null,34,41,48,null,2,6,10,null,20,22,24,null,28,30,null,39,null,42,46,null,1,4,null,null,9,11,18,null,null,null,null,null,null,null,null,null,37,null,null,44,null,47,0,null,3,null,8,null,null,13,null,19,35,38,43,null,null,null,null,null,null,null,null,null,12,null,null,null,null,36");
            return new NodeAndVal(root, 49);
        }

        public ChallengeTreeNode Run(NodeAndVal input) {
            var key = input.Val;
            var root = input.Root;

            if (root == null) {
                return null;
            }

            if (root.left == null && root.right == null) {
                if (key == root.val) {
                    return null;
                } else {
                    return root;
                }
            }

            var tempRoot = new ChallengeTreeNode(int.MinValue);
            tempRoot.right = root;

            var parentOfDelete = GetParentForKey(tempRoot, key);

            if (parentOfDelete == null) {
                return root;
            }

            var isLeft = parentOfDelete.left != null && parentOfDelete.left.val == key;

            var toDelete = isLeft ? parentOfDelete.left : parentOfDelete.right;

            var replacement = GetNextMinForNode(toDelete);

            if (replacement == null) {
                if (isLeft) {
                    parentOfDelete.left = null;
                } else {
                    parentOfDelete.right = null;
                }

                return root;
            }

            var parentOfReplacement = GetParentForKey(toDelete, replacement.val);

            var toReplaceOrphans = replacement.right != null ? replacement.right : replacement.left;

            if (toDelete.val == parentOfReplacement.val) {
                parentOfReplacement = replacement;
                toReplaceOrphans = null;
            } else {
                parentOfReplacement.left = null;
            }

            if (toDelete.right != null && replacement.val != toDelete.right.val) {
                replacement.right = toDelete.right;
            }
            if (toDelete.left != null && replacement.val != toDelete.left.val) {
                replacement.left = toDelete.left;
            }

            if (isLeft) {
                parentOfDelete.left = replacement;
            } else {
                parentOfDelete.right = replacement;
            }

            if (toReplaceOrphans != null) {
                parentOfReplacement.left = toReplaceOrphans;
            }

            return tempRoot.right;
        }

        private ChallengeTreeNode GetParentForKey(ChallengeTreeNode node, int key) {
            if ((node.left != null && node.left.val == key) || (node.right != null && node.right.val == key)) {
                return node;
            }

            if (node.left != null && key < node.val) {
                return GetParentForKey(node.left, key);
            } else if (node.right != null && key > node.val) {
                return GetParentForKey(node.right, key);
            }

            return null;
        }

        private ChallengeTreeNode GetNextMinForNode(ChallengeTreeNode node) {
            if (node.right == null) {
                if (node.left == null) {
                    return null;
                } else {
                    return node.left;
                }
            }

            var seek = node.right;

            while (seek.left != null) {
                seek = seek.left;
            }

            return seek;
        }
    }

    class NodeAndVal {
        public ChallengeTreeNode Root { get; private set; }
        public int Val { get; private set; }

        public NodeAndVal(ChallengeTreeNode root, int val) {
            Root = root;
            Val = val;
        }
    }
}

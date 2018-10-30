using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The intuition is that we can use a stack here to simulate the traversing of a tree inorder. Where as here,
    /// the # not only represent nulls but also nodes we "prune" because we visited them before
    /// </summary>
    [Challenge(Challenge.Done)]
    class VerifyPreorderSeralizationOfBinaryTree : IChallenge<string, bool> {
        public bool ExpectedOutput() {
            return true;
        }

        public string Input() {
            return "9,9,91,#,#,9,#,49,#,#,#";
        }

        public bool Run(string input) {
            var preorder = input;

            if (string.IsNullOrWhiteSpace(preorder)) {
                return false;
            }

            if (preorder[0] == '#' && preorder.Length <= 2) {
                return true;
            } else if (preorder[0] == '#') {
                return false;
            }

            var nodeStack = new Stack<string>();
            var strs = preorder.Split(',');
            for (int pos = 0; pos < strs.Length; pos++) {
                var curr = strs[pos];
                while (curr == "#" && !(nodeStack.Count == 0) && nodeStack.First() == curr) {
                    nodeStack.Pop();
                    if (nodeStack.Count == 0) {
                        return false;
                    }
                    nodeStack.Pop();
                }
                nodeStack.Push(curr);
            }
            return nodeStack.Count == 1 && nodeStack.Pop() == "#";
        }
    }
}

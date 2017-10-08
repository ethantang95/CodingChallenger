using CodingChallenger.GenericDataStructures.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Tools {
    static class TreeMaker {
        public static ChallengeNode MakeTreeFromString(string s) {
            var entries = s.Split(',').Select(r => r.Trim()).ToList();
            Queue<ChallengeNode> nodes = new Queue<ChallengeNode>();

            var root = new ChallengeNode(int.Parse(entries[0]));
            nodes.Enqueue(root);

            for (var i = 1; i < entries.Count; i+=2) {
                var node = nodes.Dequeue();
                var valLeft = entries[i];
                var valRight = entries[i + 1];

                if (valLeft != "null") {
                    var leftNode = new ChallengeNode(int.Parse(valLeft));
                    node.left = leftNode;
                    nodes.Enqueue(leftNode);
                }

                if (valRight != "null") {
                    var rightNode = new ChallengeNode(int.Parse(valRight));
                    node.right = rightNode;
                    nodes.Enqueue(rightNode);
                }
            }

            return root;
        }
    }
}

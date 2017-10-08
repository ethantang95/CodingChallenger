using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Tree {
    class ChallengeNode {
        public int val { get; private set; }
        public ChallengeNode left { get; set; }
        public ChallengeNode right { get; set; }

        public ChallengeNode(int val) {
            this.val = val;
        }

        public override string ToString() {
            var leftVal = left != null ? left.val.ToString() : "null";
            var rightVal = right != null ? right.val.ToString() : "null";
            return $"{val}, Left = {leftVal}, Right = {rightVal}";
        }

        public override bool Equals(object obj) {
            if (obj == null || obj.GetType() != GetType()) {
                return false;
            }

            var other = obj as ChallengeNode;

            if (val != other.val) {
                return false;
            }

            if ((left == null && other.left != null) || (left != null && other.left == null)) {
                return false;
            }
            if ((right == null && other.right != null) || (right != null && other.right == null)) {
                return false;
            }

            if (left != null) {
                return left.Equals(other.left);
            }
            if (right != null) {
                return right.Equals(other.right);
            }

            return true;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}

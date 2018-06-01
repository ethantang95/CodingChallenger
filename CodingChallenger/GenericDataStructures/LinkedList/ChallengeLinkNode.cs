using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.LinkedList {
    class ChallengeLinkNode {
        public int val { get; private set; }
        public ChallengeLinkNode next { get; set; }
        public ChallengeLinkNode(int x) {
            val = x;
        }

        public static ChallengeLinkNode FromInts(params int[] ints) {
            return FromInts(ints);
        }

        public static ChallengeLinkNode FromInts(IEnumerable<int> ints) {
            var root = new ChallengeLinkNode(0);
            var pointer = root;
            foreach (var val in ints) {
                pointer.next = new ChallengeLinkNode(val);
                pointer = pointer.next;
            }
            return root.next;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != GetType()) {
                return false;
            }

            var other = obj as ChallengeLinkNode;

            if ((next == null && other.next != null) || (next != null && other.next == null)) {
                return false;
            }

            if (val == other.val) {
                if (next != null && other.next != null) {
                    return next.Equals(other.next);
                }
            }

            return true;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}

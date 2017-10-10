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

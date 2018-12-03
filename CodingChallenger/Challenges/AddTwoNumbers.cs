using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using CodingChallenger.GenericDataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    ///<summary>
    ///Just treat this like how you do normal addition... always be aware of the carry
    ///Iterative through the array is usually the best solution
    /// </summary>
    [Challenge(Challenge.Done)]
    class AddTwoNumbers : ISimpleChallenge<Tuple<ChallengeLinkNode, ChallengeLinkNode>, ChallengeLinkNode> {
        public ChallengeLinkNode ExpectedOutput() {
            var result = new ChallengeLinkNode(8);

            return result;
        }

        public Tuple<ChallengeLinkNode, ChallengeLinkNode> Input() {
            var list1 = new ChallengeLinkNode(5);
            var list2 = new ChallengeLinkNode(3);

            return new Tuple<ChallengeLinkNode, ChallengeLinkNode>(list1, list2);
        }

        public ChallengeLinkNode Run(Tuple<ChallengeLinkNode, ChallengeLinkNode> input) {
            var l1 = input.Item1;
            var l2 = input.Item2;

            var completeList = new ChallengeLinkNode(0);
            var listPointer = completeList;
            var overflowToken = false;

            while (l1 != null || l2 != null) {
                var val1 = 0;
                var val2 = 0;

                if (l1 != null) {
                    val1 = l1.val;
                    l1 = l1.next;
                }

                if (l2 != null) {
                    val2 = l2.val;
                    l2 = l2.next;
                }

                var result = CalculateOneDigit(val1, val2);
                var digit = result.Item1 + (overflowToken ? 1 : 0);
                overflowToken = result.Item2;

                if (digit > 9) {
                    digit = digit % 10;
                    overflowToken = true;
                }

                listPointer.next = new ChallengeLinkNode(digit);
                listPointer = listPointer.next;
            }

            if (overflowToken) {
                listPointer.next = new ChallengeLinkNode(1);
            }

            return completeList.next;

        }

        private Tuple<int, bool> CalculateOneDigit(int val1, int val2) {
            if (val1 + val2 > 9) {
                return new Tuple<int, bool>((val1 + val2) % 10, true);
            } else {
                return new Tuple<int, bool>(val1 + val2, false);
            }
        }
    }
}

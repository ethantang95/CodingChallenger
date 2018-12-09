using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Keep track of 2 sequences now! YAY!!!!!!
    /// </summary>
    [Challenge(Challenge.Done)]
    class MaxConsecutiveOnesII : ISimpleChallenge<int[], int> {
        public int ExpectedOutput() {
            return 5;
        }

        public int[] Input() {
            return new int[] { 0, 1, 1, 1, 1, };
        }

        public int Run(int[] input) {
            var nums = input;

            var currentConsecutiveSequence = 0;
            var currentConsecutiveSequenceWithFlip = 0;
            var maxSequence = 0;

            foreach (var num in nums) {
                if (num == 1) {
                    currentConsecutiveSequence++;
                    currentConsecutiveSequenceWithFlip++;
                } else {
                    maxSequence = Math.Max(maxSequence, currentConsecutiveSequenceWithFlip);

                    currentConsecutiveSequenceWithFlip = currentConsecutiveSequence + 1;
                    currentConsecutiveSequence = 0;
                }
            }

            maxSequence = Math.Max(maxSequence, currentConsecutiveSequenceWithFlip);
            return maxSequence;
        }
    }
}

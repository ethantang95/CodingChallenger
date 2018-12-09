using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class MaxConsecutiveOnes : ISimpleChallenge<int[], int> {
        public int ExpectedOutput() {
            return 3;
        }

        public int[] Input() {
            return new int[] { 1, 1, 0, 1, 1, 1 };
        }

        public int Run(int[] input) {
            var nums = input;

            var highestOnesCount = 0;
            var onesCount = 0;
            foreach (var num in nums) {
                if (num == 1) {
                    onesCount++;
                } else {
                    highestOnesCount = Math.Max(highestOnesCount, onesCount);
                    onesCount = 0;
                }
            }

            highestOnesCount = Math.Max(highestOnesCount, onesCount);
            return highestOnesCount;
        }
    }
}

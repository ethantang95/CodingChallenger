using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Kadane's, read it
    /// </summary>
    [Challenge(Challenge.Done)]
    class MaxSubArray : IChallenge<int[], int> {
        public int ExpectedOutput() {
            return 6;
        }

        public int[] Input() {
            return new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
        }

        public int Run(int[] input) {
            var nums = input;

            var bestMax = 0;
            var currentMax = 0;

            foreach (var num in nums) {
                currentMax = Math.Max(currentMax + num, num);
                if (bestMax < currentMax) {
                    bestMax = currentMax;
                }
            }

            if (bestMax == 0) {
                bestMax = nums.Max();
            }

            return bestMax;
        }
    }
}

using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Greedy, take the jump if it is higher... also watch out for end conditions
    /// </summary>
    [Challenge(Challenge.Done)]
    class JumpGame : ISimpleChallenge<int[], bool> {
        public bool ExpectedOutput() {
            return false;
        }

        public int[] Input() {
            return new int[5] { 3, 2, 1, 0, 4 };
        }

        public bool Run(int[] input) {
            var nums = input;

            if (nums.Length <= 1) {
                return true;
            }

            var idx = 0;
            var jumps = nums[idx];

            while (jumps > 0 || nums[idx] > 0) {
                if (nums[idx] > jumps) {
                    jumps = nums[idx];
                } else {
                    jumps--;
                    idx++;
                }
                if (idx == nums.Length - 1) {
                    break;
                }
            }

            return idx == nums.Length - 1;
        }
    }
}

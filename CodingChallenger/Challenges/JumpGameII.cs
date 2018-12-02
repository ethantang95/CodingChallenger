using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This is a greedy problem but the question is what do you be greedy about...
    /// 
    /// In a way, this can be portrayed as a BFS problem also but the trick here is to
    /// understand what each number at each index represents.
    /// 
    /// At a given index i, nums[i] is how many jumps it can give, effectively meaning
    /// what is the range of that given space. If we can look at each spot as the range
    /// then we will just take all the maximum range that is accessible by being at index i.
    /// That is the greedy part, taking the max range available at each jump.
    /// </summary>
    [Challenge(Challenge.Done)]
    class JumpGameII : IChallenge<int[], int> {
        public int ExpectedOutput() {
            return 2;
        }

        public int[] Input() {
            return new int[] { 4, 1, 1, 3, 1 ,1 };
        }

        public int Run(int[] input) {
            var nums = input;

            var jumps = 0;
            // the array will be scanned through dfs jumps
            var currentIdx = 0;
            // when we have scanned through the entire array
            while (currentIdx < nums.Length - 1) {
                // get how many indices can we jump to
                var survey = nums[currentIdx];
                // find the value that can get us the farthest in that range
                var maxRange = 0;
                var maxIdx = 0;
                for (var surveyIdx = currentIdx + 1; surveyIdx < currentIdx + survey + 1; surveyIdx++) {
                    // the survey is out of bounds, meaning on the current hop, we can get to the end already
                    if (surveyIdx >= nums.Length - 1) {
                        jumps++;
                        return jumps;
                    }

                    // find the range with the current survey
                    var range = nums[surveyIdx] + currentIdx + (surveyIdx - currentIdx);
                    if (range >= maxRange) {
                        maxRange = range;
                        maxIdx = surveyIdx;
                    }
                }
                jumps++;
                currentIdx = maxIdx;
            }

            return jumps;
        }
    }
}

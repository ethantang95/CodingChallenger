using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.DoNotRun)]
    class LongestSubstringWithoutRepeatChar : IChallenge<string, int> {
        public int ExpectedOutput() {
            return 1;
        }

        public string Input() {
            return "aaa";
        }

        public int Run(string input) {
            //chars goes up to 256
            var charTracker = new int[256];
            var max = -1;
            var currPos = 0;
            charTracker = ResetIntArray(charTracker, -1);

            for (var i = 0; i < input.Length; i++) {
                if (charTracker[input[i]] > currPos) {
                    max = Math.Max(i - currPos, max);
                    currPos = i;
                    ResetIntArray(charTracker, currPos);
                }

                charTracker[input[i]] = i;
            }
            if (input[input.Length - 1] == input[input.Length - 2]) {
                return Math.Max(max, input.Length - currPos - 1);
            }
            return Math.Max(max, input.Length - currPos);
        }

        private int[] ResetIntArray(int[] arr, int val) {
            for (var i = 0; i < arr.Length; i++) {
                if (val == -1) {
                    arr[i] = val;
                } else if (arr[i] <= val) {
                    arr[i] = val;
                }
            }
            return arr;
        }
    }
}

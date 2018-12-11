using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This is basically a sliding window problem while keeping track of what is inside the window
    /// Use a hash table to keep track of the chars, should be O(1) since there is a finite set of chars
    /// that exists in the char space
    /// </summary>
    [Challenge(Challenge.Done)]
    class LongestSubstringWithKDistrinctChar : ISimpleChallenge<(string s, int k), int> {
        public int ExpectedOutput() {
            return 5;
        }

        public (string s, int k) Input() {
            return ("eeebaaaa", 2);
        }

        public int Run((string s, int k) input) {
            var s = input.s;
            var k = input.k;

            // sliding window problem
            var head = 0;
            var tail = 0;
            var maxSubstringLength = 0;
            var charTracker = new Dictionary<char, int>();

            while (head < s.Length) {
                var cHead = s[head];
                if (charTracker.ContainsKey(cHead)) {
                    charTracker[cHead]++;
                } else {
                    charTracker.Add(cHead, 1);

                    if (charTracker.Count > k) {
                        // take a snapshot
                        maxSubstringLength = Math.Max(maxSubstringLength, head - tail);
                        while (charTracker.Count > k) {
                            var cTail = s[tail];

                            charTracker[cTail]--;
                            if (charTracker[cTail] == 0) {
                                charTracker.Remove(cTail);
                            }
                            tail++; 
                        }
                    }
                }
                head++;
            }

            maxSubstringLength = Math.Max(maxSubstringLength, head - tail);
            return maxSubstringLength;
        }
    }
}

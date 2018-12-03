using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class FirstUniqChar : ISimpleChallenge<string, int> {
        public int ExpectedOutput() {
            return -1;
        }

        public string Input() {
            return "aabbccddeeffgghhiijjkkllmmnnooppqqrrssttuuvvwwxxyyzz";
        }

        public int Run(string input) {
            var s = input;

            var charTracker = new int[26];
            for (var i = 0; i < charTracker.Length; i++) {
                charTracker[i] = -2;
            }

            var seenCount = 0;
            // let's encode with -2 meaning haven't seen yet, -1 meaning seen, >=0 is first appearance
            // also keep track if we seen duplicates for all letters for short circuiting

            for (var i = 0; i < s.Length; i++) {
                var charSlot = s[i] - 'a';
                if (charTracker[charSlot] == -2) {
                    charTracker[charSlot] = i;
                } else if (charTracker[charSlot] > -1) {
                    charTracker[charSlot] = -1;
                    seenCount++;
                    if (seenCount >= 26) {
                        return -1;
                    }
                }
            }

            var min = int.MaxValue;
            for (var i = 0; i < charTracker.Length; i++) {
                if (charTracker[i] > -1 && charTracker[i] < min) {
                    min = charTracker[i];
                }
            }

            return min == int.MaxValue ? -1 : min;
        }
    }
}

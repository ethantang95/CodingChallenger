using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Simple Brute Force. Could use KMP for better run time
    /// </summary>
    [Challenge(Challenge.Done)]
    class ShortestPalindrome : IChallenge<string, string> {
        public string ExpectedOutput() {
            return "aba";
        }

        public string Input() {
            return "aba";
        }

        public string Run(string input) {
            var prefix = FindLongestPrefixPalin(input);
            var suffix = input.Substring(prefix.Length);
            suffix = new string(suffix.Reverse().ToArray());
            return suffix + input;
        }

        private string FindLongestPrefixPalin(string s) {
            var frontPtr = 0;
            var backPtr = s.Length - 1;
            var backPtrOrig = backPtr;

            while (frontPtr < backPtr) {
                if (s[frontPtr] == s[backPtr]) {
                    frontPtr++;
                    backPtr--;
                } else {
                    frontPtr = 0;
                    backPtrOrig--;
                    backPtr = backPtrOrig;
                }
            }

            return s.Substring(0, backPtrOrig + 1);
        }
    }
}

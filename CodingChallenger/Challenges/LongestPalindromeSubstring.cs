using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Take away is adding a char in between each character to make it easier and becoming O(n^2) time
    /// </summary>
    [Challenge(Challenge.Done)]
    class LongestPalindromeSubstring : IChallenge<string, string> {
        public string ExpectedOutput() {
            return "bb";
        }

        public string Input() {
            return "abb";
        }

        public string Run(string input) {
            var longestPalin = "";

            for (var i = 0; i < input.Length; i++) {
                var palinOdd = FindPalin(i, i, input);
                if (palinOdd.Length > longestPalin.Length) {
                    longestPalin = palinOdd;
                }

                if (i + 1 < input.Length) {
                    var palinEven = FindPalin(i, i + 1, input);
                    if (palinEven.Length > longestPalin.Length) {
                        longestPalin = palinEven;
                    }
                }
            }

            return longestPalin;
        }

        private string FindPalin(int leftPtr, int rightPtr, string s) {

            while (leftPtr >= 0 && rightPtr < s.Length && s[leftPtr] == s[rightPtr]) {
                leftPtr--;
                rightPtr++;
            }

            leftPtr++;
            rightPtr--;

            if (rightPtr - leftPtr < 0) {
                return "";
            }

            return s.Substring(leftPtr, rightPtr - leftPtr + 1);
        }
    }
}

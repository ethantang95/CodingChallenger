using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The take away is that to ask questions about the bounds of this, what the input type is
    /// then handling the input type.
    /// 
    /// also to know about the properties of the palindrome
    /// </summary>
    [Challenge(Challenge.Done)]
    class CanPermutePalindrome : IChallenge<string, bool> {
        public bool ExpectedOutput() {
            return false;
        }

        public string Input() {
            return "AaBb//a";
        }

        public bool Run(string input) {
            if (string.IsNullOrEmpty(input)) {
                return false;
            }

            var charArray = new int[256];

            foreach (var c in input) {
                charArray[c]++;
            }

            bool oneOdd = false;

            foreach (var count in charArray) {
                if (count % 2 == 1) {
                    if (oneOdd) {
                        return false;
                    } else {
                        oneOdd = true;
                    }
                }
            }

            return true;
        }
    }
}

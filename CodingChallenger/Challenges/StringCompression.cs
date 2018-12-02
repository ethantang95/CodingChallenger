using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.NotDone)]
    class StringCompression : IChallengeModifyInput<char[], int> {
        public bool AssertResult(char[] expected, char[] actual, int output) {
            for (var i = 0; i < output; i++) {
                if (expected[i] != actual[i]) {
                    return false;
                }
            }
            return true;
        }

        public char[] ExpectedModifiedInput() {
            return new char[] { 'a', '2', 'b', '2', 'c', '3' };
        }

        public int ExpectedOutput() {
            return 6;
        }

        public char[] Input() {
            return new char[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c' };
        }

        public int Run(char[] input) {
            return 6;
        }
    }
}

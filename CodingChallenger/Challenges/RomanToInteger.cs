using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The take away from this is noticing that the order of how Roman numerals are...
    /// if the previous value is less than the next value, then it is a subtraction
    /// else it is an addition
    /// </summary>
    [Challenge(Challenge.Done)]
    class RomanToInteger : ISimpleChallenge<string, int> {

        Dictionary<char, int> _romanTable;

        public RomanToInteger() {
            _romanTable = new Dictionary<char, int>();
            _romanTable.Add('M', 1000);
            _romanTable.Add('D', 500);
            _romanTable.Add('C', 100);
            _romanTable.Add('L', 50);
            _romanTable.Add('X', 10);
            _romanTable.Add('V', 5);
            _romanTable.Add('I', 1);
        }

        public int ExpectedOutput() {
            return 3496;
        }

        public string Input() {
            return "MMMCDXCVI";
        }

        public int Run(string input) {
            if (string.IsNullOrEmpty(input)) {
                return 0;
            }
            var val = 0;

            var prev = input[0];

            for (var i = 1; i < input.Length; i++) {
                var curr = input[i];
                if (_romanTable[prev] < _romanTable[curr]) {
                    val -= _romanTable[prev];
                } else {
                    val += _romanTable[prev];
                }

                prev = curr;
            }

            val += _romanTable[prev];

            return val;
        }
    }
}

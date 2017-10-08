using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The take away for this is that we want to detect a cycle, this is because if
    /// the value x is calculated to become y, then if we get x again, we know that 
    /// since this function is a 1 way function and y will be the solution.
    /// This implies getting any of our previous solution would end up in a cycle.
    /// 
    /// The solution either keep track of previous results, which this one does and it takes
    /// O(n) memory, or we can use Flyod cycles which can use O(1) memory
    /// 
    /// Both solution uses O(n) runtime
    /// </summary>
    [Challenge(Challenge.Done)]
    class HappyNumber : IChallenge<int, bool> {
        public bool ExpectedOutput() {
            return true;
        }

        public int Input() {
            return 1;
        }

        public bool Run(int input) {
            var pastAnswer = new HashSet<int>();

            do {
                var digits = GetDigits(input);
                input = HappyCalculator(digits);
                if (pastAnswer.Contains(input)) {
                    return false; //cycle
                } else {
                    pastAnswer.Add(input);
                }
            } while (input != 1);

            return true;
        }

        private List<int> GetDigits(int input) {
            var inputString = input + "";
            var inputDigits = new List<int>();
            foreach (var c in inputString) {
                inputDigits.Add(int.Parse(c + ""));
            }

            return inputDigits;
        }

        private int HappyCalculator(List<int> digits) {
            var sum = 0;
            foreach (var digit in digits) {
                sum += digit * digit;
            }

            return sum;
        }
    }
}

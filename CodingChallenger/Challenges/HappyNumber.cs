using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
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

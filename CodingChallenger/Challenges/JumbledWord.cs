using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.DoNotRun)]
    class JumbledWord : ISimpleChallenge<Tuple<string, string>, bool> {
        public bool ExpectedOutput() {
            return true;
        }

        public Tuple<string, string> Input() {
            return new Tuple<string, string>("tommarvoloriddle", "iamlordvoldemort");
        }

        public bool Run(Tuple<string, string> input) {
            var a = input.Item1;
            var b = input.Item2;

            var letterTracker = new Dictionary<char, int>();

            foreach (var c in a) {
                if (!letterTracker.ContainsKey(c)) {
                    letterTracker.Add(c, 0);
                }
                letterTracker[c]++;
            }

            foreach (var c in b) {
                if (!letterTracker.ContainsKey(c)) {
                    return false;
                } else {
                    letterTracker[c]--;
                }
            }

            foreach (var frequency in letterTracker.Values) {
                if (frequency != 0) {
                    return false;
                }
            }

            return true;
        }
    }
}

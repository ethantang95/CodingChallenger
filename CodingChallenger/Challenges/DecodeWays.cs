using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The intuition of this is to observe the many different scenarios there can be
    /// and the dynamic programming part is knowing that what happens with the current number
    /// is dependent on the results generated from the previous number
    /// </summary>
    [Challenge(Challenge.Done)]
    class DecodeWays : IChallenge<string, int> {
        public int ExpectedOutput() {
            return 2;
        }

        public string Input() {
            return "17";
        }

        public int Run(string input) {
            var s = input;

            if (string.IsNullOrWhiteSpace(s) || s[0] == '0') {
                return 0;
            } else if (s.Length == 1) {
                return 1;
            }

            // turn this into an int array
            var sInts = s.Select(c => c - '0').ToList();

            // first, we want an array to keep track of how many more combinations can that number
            // contribute
            var staticCount = new int[sInts.Count]; // count of how many entries are static
            var manipulatableCount = new int[sInts.Count]; // count of how many entries are manipulatable
            if (sInts[0] <= 2) {
                manipulatableCount[0] = 1;
            } else {
                staticCount[0] = 1;
            }

            for (var i = 1; i < sInts.Count; i++) {
                var val = sInts[i];
                if (val == 0) {
                    // case 1, the val is 0, aka a hole
                    // statics cannot propogate, only manipulable can and they turn into static
                    staticCount[i] = manipulatableCount[i - 1];
                    manipulatableCount[i] = 0;
                    // if there are no manipulable, then return 0
                    if (staticCount[i] == 0) {
                        return 0;
                    }
                } else if (val == 1 || val == 2) {
                    // case 2, the number is 1 or 2, which can manipulate, and also create manipulables
                    // the amount of static ones this create is equal to how many manipulables were prior
                    // because the manipulable ones are manipulated
                    staticCount[i] = manipulatableCount[i - 1];
                    // the amount of manipulable ones this create is equal to both the static and manipulables
                    manipulatableCount[i] = staticCount[i - 1] + manipulatableCount[i - 1];
                } else if ((val > 2 && val <= 6) || (sInts[i - 1] == 1)) {
                    // case 3, the letters where they cannot create new manipulables, but can manipulate
                    // the amount of static ones this creates is equal to static and 2 * manipulable
                    // because once for the manipulable to be manipulated, and the other doesn't
                    staticCount[i] = staticCount[i - 1] + (2 * manipulatableCount[i - 1]);
                    manipulatableCount[i] = 0;
                } else if (val > 6 && val <= 9) {
                    // case 4, the letter cannot manipulable nor create new manipulables
                    // the amount of static ones this create is equal to static and manipulables
                    staticCount[i] = staticCount[i - 1] + manipulatableCount[i - 1];
                    manipulatableCount[i] = 0;
                } else {
                    // case 5... wtf?
                    Console.WriteLine($"WTF? {val} at {i}");
                }
            }

            return staticCount.Last() + manipulatableCount.Last();

        }
    }
}

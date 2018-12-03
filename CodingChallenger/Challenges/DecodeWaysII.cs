using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Same as DecodeWay but need to count all the occurance with the *s
    /// 
    /// also there is a problem here with modular arithmetics where mod(a + b) is not the same as mod(mod(a) + mod(b))
    /// </summary>
    [Challenge(Challenge.Done)]
    class DecodeWaysII : ISimpleChallenge<string, int> {
        public int ExpectedOutput() {
            return 104671669;
        }

        public string Input() {
            return "*******************";
        }

        public int Run(string input) {
            var s = input;

            if (string.IsNullOrWhiteSpace(s) || s[0] == '0') {
                return 0;
            } else if (s.Length == 1) {
                return s[0] == '*' ? 9 : 1;
            }

            // turn this into an int array
            var sInts = s.Select(c => c != '*' ? c - '0' : -1).ToList();

            // first, we want an array to keep track of how many more combinations can that number
            // contribute
            var staticCount = new long[sInts.Count]; // count of how many entries are static
            var manipulatableCount = new long[sInts.Count]; // count of how many entries are manipulatable
            if (sInts[0] == 2 || sInts[0] == 1) {
                manipulatableCount[0] = 1;
            } else if (sInts[0] == -1) {
                manipulatableCount[0] = 2;
                staticCount[0] = 7;
            } else {
                staticCount[0] = 1;
            }

            for (var i = 1; i < sInts.Count; i++) {
                var val = sInts[i];
                if (sInts[i - 1] == -1) {
                    if (val == -1) {
                        var staticCountStar = 0L;
                        var manipulatableCountStar = 0L;

                        staticCountStar += 2 * manipulatableCount[i - 1];
                        manipulatableCountStar += 2 * (staticCount[i - 1] + manipulatableCount[i - 1]);

                        staticCountStar += 4 * (staticCount[i - 1] + (2 * manipulatableCount[i - 1]));

                        staticCountStar += 3 * (staticCount[i - 1] + manipulatableCount[i - 1] + (manipulatableCount[i - 1] / 2));

                        staticCount[i] = Sanitize(staticCountStar);
                        manipulatableCount[i] = Sanitize(manipulatableCountStar);
                    } else if (val == 0) {
                        // same as normal case
                        staticCount[i] = Sanitize(manipulatableCount[i - 1]);
                        manipulatableCount[i] = 0;
                        if (staticCount[i] == 0) {
                            return 0;
                        }
                    } else if (val == 1 || val == 2) {
                        // same as normal case
                        staticCount[i] = manipulatableCount[i - 1];
                        manipulatableCount[i] = Sanitize(staticCount[i - 1] + manipulatableCount[i - 1]);
                    } else if (val > 2 && val <= 6) {
                        // same as normal case
                        staticCount[i] = Sanitize(staticCount[i - 1] + (2 * manipulatableCount[i - 1]));
                        manipulatableCount[i] = 0;
                    } else if (val > 6 && val <= 9) {
                        // different, half of manip can be manipulated, rest goes into static
                        staticCount[i] = Sanitize(staticCount[i - 1] + manipulatableCount[i - 1] + (manipulatableCount[i - 1] / 2));
                        manipulatableCount[i] = 0;
                    } else {
                        Console.WriteLine($"WTF? {val} at {i}");
                    }
                } else {
                    if (val == -1) {
                        // case 1, when we get a *, thankfully we basically run the previous sequences again
                        var staticCountStar = 0L;
                        var manipulatableCountStar = 0L;

                        // the repeat of case 3:
                        staticCountStar += 2 * manipulatableCount[i - 1];
                        manipulatableCountStar += 2 * (staticCount[i - 1] + manipulatableCount[i - 1]);
                        if (sInts[i - 1] == 1) {
                            // case 4 blanket:
                            staticCountStar += 7 * (staticCount[i - 1] + (2 * manipulatableCount[i - 1]));
                        } else {
                            // case 4 only half:
                            staticCountStar += 4 * (staticCount[i - 1] + (2 * manipulatableCount[i - 1]));
                            // case 5:
                            staticCountStar += 3 * (staticCount[i - 1] + manipulatableCount[i - 1]);
                        }
                        staticCount[i] = Sanitize(staticCountStar);
                        manipulatableCount[i] = Sanitize(manipulatableCountStar);
                    } else if (val == 0) {
                        // case 2, the val is 0, aka a hole
                        // statics cannot propogate, only manipulable can and they turn into static
                        staticCount[i] = Sanitize(manipulatableCount[i - 1]);
                        manipulatableCount[i] = 0;
                        // if there are no manipulable, then return 0
                        if (staticCount[i] == 0) {
                            return 0;
                        }
                    } else if (val == 1 || val == 2) {
                        // case 3, the number is 1 or 2, which can manipulate, and also create manipulables
                        // the amount of static ones this create is equal to how many manipulables were prior
                        // because the manipulable ones are manipulated
                        staticCount[i] = manipulatableCount[i - 1];
                        // the amount of manipulable ones this create is equal to both the static and manipulables
                        manipulatableCount[i] = Sanitize(staticCount[i - 1] + manipulatableCount[i - 1]);
                    } else if ((val > 2 && val <= 6) || (sInts[i - 1] == 1)) {
                        // case 4, the letters where they cannot create new manipulables, but can manipulate
                        // the amount of static ones this creates is equal to static and 2 * manipulable
                        // because once for the manipulable to be manipulated, and the other doesn't
                        staticCount[i] = Sanitize(staticCount[i - 1] + (2 * manipulatableCount[i - 1]));
                        manipulatableCount[i] = 0;
                    } else if (val > 6 && val <= 9) {
                        // case 5, the letter cannot manipulable nor create new manipulables
                        // the amount of static ones this create is equal to static and manipulables
                        staticCount[i] = Sanitize(staticCount[i - 1] + manipulatableCount[i - 1]);
                        manipulatableCount[i] = 0;
                    } else {
                        // case 6... wtf?
                        Console.WriteLine($"WTF? {val} at {i}");
                    }
                }
            }

            return (int)SanitizeAns(staticCount.Last() + manipulatableCount.Last());
        }

        private long Sanitize(long i) {
            return i % (0xFFFFFFL * (1000000000L + 7L));
        }

        private long SanitizeAns(long i) {
            return i % (1000000000 + 7);
        }
    }
}

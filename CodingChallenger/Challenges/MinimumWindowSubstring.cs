using CodingChallenger.Framework;
using CodingChallenger.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// So this was some complicated logical shit...
    /// 
    /// The idea of this is to use a sliding window. One thing to know is that the t can have repeated letters so 2 things needed to be tracked of
    /// 1. How many times has each individual letter been seen
    /// 2. How many letters in the substring overall are accounted for
    /// 
    /// This is why we require a complicated counter. For example, if a letter have been seen more times than it appears in the s string
    /// then we can consider that as redundent
    /// 
    /// When a letter is redundent, that means the back sliding window can skip over it, the front sliding window over it does not contribute to the
    /// overall account
    /// 
    /// The idea is that we need to figure out where our initial sliding window is to establish the upperlimit of how big our sliding window is
    /// After we have that, we will start sliding 1 by 1, because the subsequent valid sliding windows are smaller or equal to our initial.
    /// 
    /// First, we need to keep track if the substring that is in our window is one that contains the letters of t. If it doesn't, then we
    /// will keep sliding. If it does, we should then trim the back of the window so that it becomes smaller.
    /// 
    /// This is ran until the entire string has been iterated through
    /// </summary>
    [Challenge(Challenge.Done)]
    class MinimumWindowSubstring : IChallenge<Tuple<string, string>, string> {
        public string ExpectedOutput() {
            return "aec";
        }

        public Tuple<string, string> Input() {
            return new Tuple<string, string>("cabefgecdaecf", "cae");
        }

        public string Run(Tuple<string, string> input) {
            var s = input.Item1;
            var t = input.Item2;

            if (s == "") {
                return "";
            }
            if (t == "") {
                return "";
            }

            if (t.Length > s.Length) {
                return "";
            }

            var letterTracker = new Dictionary<char, Pair<int, int>>();
            foreach (var letter in t) {
                if (!letterTracker.ContainsKey(letter)) {
                    letterTracker[letter] = new Pair<int, int>();
                }
                letterTracker[letter].Item2++;
            }

            var front = 0;
            var back = 0;
            var bestFront = 0;
            var bestBack = 0;
            var containsCount = 0;

            // the first time population of the window
            while (containsCount < t.Length && front < s.Length) {
                var letter = s[front];
                if (letterTracker.ContainsKey(letter)) {
                    letterTracker[letter].Item1++;
                    if (letterTracker[letter].Item1 <= letterTracker[letter].Item2) {
                        containsCount++;
                    }
                }
                front++;
            }

            // cannot find a suitable substring
            if (containsCount != t.Length) {
                return "";
            } else {
                var windowFound = false;
                while (!windowFound) {
                    var letter = s[back];
                    if (letterTracker.ContainsKey(letter)) {
                        // redundant character, we can remove
                        if (letterTracker[letter].Item1 > letterTracker[letter].Item2) {
                            letterTracker[letter].Item1--;
                        } else {
                            // non redundent character, this is our first window
                            windowFound = true;
                            bestFront = front;
                            bestBack = back;
                            break;
                        }
                    }
                    back++; // if this is done, the window is ironically now not complete which is good
                }
            }

            // the window size should not grow, only stay the same, or shrink
            while (front < s.Length) {
                // let the back move first
                back++;
                var backLetter = s[back - 1];
                if (letterTracker.ContainsKey(backLetter)) {
                    if (letterTracker[backLetter].Item1 <= letterTracker[backLetter].Item2) {
                        containsCount--;
                    }
                    letterTracker[backLetter].Item1--;
                }

                // moving the front sliding window
                var frontLetter = s[front];
                if (letterTracker.ContainsKey(frontLetter)) {
                    if (letterTracker[frontLetter].Item1 < letterTracker[frontLetter].Item2) {
                        containsCount++;
                    }
                    letterTracker[frontLetter].Item1++;
                }
                front++;

                // we found another subset, trim the back though
                if (containsCount == t.Length) {
                    var shrunkEnough = false;
                    while (!shrunkEnough) {
                        backLetter = s[back];
                        if (letterTracker.ContainsKey(backLetter)) {
                            if (letterTracker[backLetter].Item1 > letterTracker[backLetter].Item2) {
                                letterTracker[backLetter].Item1--;
                            } else {
                                // shouldn't shrink anymore
                                var substrLength = front - back;
                                if (substrLength < (bestFront - bestBack)) {
                                    bestBack = back;
                                    bestFront = front;
                                }
                                break;
                            }
                        }
                        back++;
                    }
                }
            }

            return s.Substring(bestBack, (bestFront - bestBack));
        }
    }
}

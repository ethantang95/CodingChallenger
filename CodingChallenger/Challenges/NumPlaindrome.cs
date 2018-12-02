using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class NumPlaindrome : IChallenge<string, int> {
        public int ExpectedOutput() {
            return 3;
        }

        public string Input() {
            return "abcabc";
        }

        public int Run(string input) {
            var s = input;
            // first make the string to ?#?#?#... format
            var charArray = new char[(s.Length * 2) + 1];
            charArray[0] = '#';
            var sCounter = 0;
            var palinHash = new HashSet<string>();
            for (var i = 1; i < charArray.Length; i += 2) {
                charArray[i] = s[sCounter];
                charArray[i + 1] = '#';
                sCounter++;
            }

            var numPalindromes = 0;
            for (var i = 0; i < charArray.Length; i++) {
                var center = i;
                var spread = 0;
                while ((i - spread) >= 0 && (i + spread) < charArray.Length && (charArray[center + spread] == charArray[center - spread])) {
                    if (spread != 0) {
                        var subString = new String(charArray, center - spread, (spread * 2) + 1);
                        subString = subString.Replace("#", "");
                        if (!palinHash.Contains(subString)) {
                            numPalindromes++;
                            palinHash.Add(subString);
                        }
                    }
                    spread++;
                }
            }

            return numPalindromes;

        }
    }
}

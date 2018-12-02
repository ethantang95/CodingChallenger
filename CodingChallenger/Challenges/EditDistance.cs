using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class EditDistance : IChallenge<Tuple<string, string>, int> {
        public int ExpectedOutput() {
            return 10;
        }

        public Tuple<string, string> Input() {
            return new Tuple<string, string>("zoologicoarchaeologist", "zoogeologist");
        }

        public int Run(Tuple<string, string> input) {
            var word1 = input.Item1;
            var word2 = input.Item2;

            var changeGrid = new int[word1.Length + 1, word2.Length + 1];

            // populate the empty string spaces first
            for (var i = 0; i <= word1.Length; i++) {
                changeGrid[i, 0] = i;
            }
            for (var i = 0; i <= word2.Length; i++) {
                changeGrid[0, i] = i;
            }

            // calculate the individual cells representing how many changes it requires for that substring to become this substring
            for (var i = 1; i <= word1.Length; i++) {
                for (var j = 1; j <= word2.Length; j++) {
                    if (word1[i - 1] == word2[j - 1]) {
                        var minChanges = 1 + Math.Min(Math.Min(changeGrid[i - 1, j], changeGrid[i, j - 1]), changeGrid[i - 1, j - 1] - 1);
                        changeGrid[i, j] = minChanges;
                    } else {
                        var minChanges = 1 + Math.Min(Math.Min(changeGrid[i - 1, j], changeGrid[i, j - 1]), changeGrid[i - 1, j - 1]);
                        changeGrid[i, j] = minChanges;
                    }
                }
            }

            return changeGrid[word1.Length, word2.Length];
        }
    }
}

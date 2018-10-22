using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The first way is brute force, basically after each day, scan through the blooming array to see if it satisfies k
    /// However, this is not really efficient because it is essentially O(n^2). The intuition here is to really
    /// shrink up the search space for each time we observe a new flower blooming
    /// There are 2 simplier and intuitive solutions that is O(nlogn) and O(nk)... There is an O(n) solution that utilizes the sliding
    /// window technique
    /// For the O(nlogn) solution, the idea is that when a new flower blooms, we check where is the first leftest and rightest flowers
    /// to see if it satisfies the k criterion. To do that, we need a TreeSet which is basically a BST. for the new flower, we check
    /// the lower and the upper of the input and calculate the difference between those against the input (blooming slot).
    /// Unfortunately, C# does not have such abilities that Java's TreeSet have so therefore it is not possible to implement this with
    /// C#'s existing data structures
    /// The O(nk) solution is that we should only search the status of the flowers at the blooming spot +k and -k. If nothing between
    /// them are blooming, then it is valid... if there are things blooming, then it is invalid
    /// </summary>
    [Challenge(Challenge.Done)]
    class KEmptySlots : IChallenge<Tuple<int[], int>, int> {
        public int ExpectedOutput() {
            return -1;
        }

        public Tuple<int[], int> Input() {
            return new Tuple<int[], int>(new int[] { 1, 2, 3 }, 1);
        }

        public int Run(Tuple<int[], int> input) {
            var flowers = input.Item1;
            var k = input.Item2 + 1;

            var bloomIndices = new bool[flowers.Length];

            for (var i = 0; i < flowers.Length; i++) {
                var bloomIndex = flowers[i] - 1;
                bloomIndices[bloomIndex] = true;
                if (bloomIndex + k < flowers.Length && bloomIndices[bloomIndex + k]) {
                    var valid = true;
                    for (var j = bloomIndex + k - 1; j > bloomIndex; j--) {
                        if (bloomIndices[j]) {
                            valid = false;
                            break;
                        }
                    }
                    if (valid) {
                        return i + 1;
                    }
                }
                if (bloomIndex - k >= 0 && bloomIndices[bloomIndex - k]) {
                    var valid = true;
                    for (var j = bloomIndex - k + 1; j < bloomIndex; j++) {
                        if (bloomIndices[j]) {
                            valid = false;
                            break;
                        }
                    }
                    if (valid) {
                        return i + 1;
                    }
                }
            }

            return -1;
        }
    }
}

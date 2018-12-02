using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Hash tables
    /// </summary>
    [Challenge(Challenge.Done)]
    class IntersectTwoArrays : IChallenge<(int[] num1, int[] num2), int[]> {
        public int[] ExpectedOutput() {
            return new int[] { 9, 4 };
        }

        public (int[] num1, int[] num2) Input() {
            return (new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 });
        }

        public int[] Run((int[] num1, int[] num2) input) {
            (var num1, var num2) = input;

            var intersectionSet = new HashSet<int>();
            var toReturn = new HashSet<int>();

            foreach (var num in num1) {
                intersectionSet.Add(num);
            }

            foreach (var num in num2) {
                if (intersectionSet.Contains(num)) {
                    toReturn.Add(num);
                }
            }

            return toReturn.ToArray();
        }
    }
}

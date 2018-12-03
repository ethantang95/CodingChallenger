using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class FruitTree : ISimpleChallenge<int[], int> {
        public int ExpectedOutput() {
            return 7;
        }

        public int[] Input() {
            return new int[] { 1, 2, 1, 2, 1, 2, 1 };
        }

        public int Run(int[] input) {
            var A = input;

            if (A.Length == 0) {
                return 0;
            }

            var currSequence = 1;
            var continuousFruit = 1;
            var max = 1;
            var lastSeen = A[0];
            var fruitTracker = new HashSet<int>();
            fruitTracker.Add(lastSeen);

            for (var i = 1; i < A.Length; i++) {
                var fruit = A[i];
                if (fruit == lastSeen) {
                    // continuous, keep on counting
                    continuousFruit++;
                    currSequence++;
                } else if (fruitTracker.Count == 1) {
                    // a new fruit appeared, BUT we don't have 2 different fruits yet
                    lastSeen = fruit;
                    fruitTracker.Add(lastSeen);
                    continuousFruit = 1;
                    currSequence++;
                } else if (fruitTracker.Contains(fruit)) {
                    // this type of fruit is inside our fruit tracking, but it is different from what we last seen
                    lastSeen = fruit;
                    continuousFruit = 1;
                    currSequence++;
                } else if (!fruitTracker.Contains(fruit)) {
                    // a new fruit appeared and we have seen 2 other fruits so far

                    // first, we have to add up what have we seen
                    max = Math.Max(currSequence, max);
                    currSequence = continuousFruit;

                    // next, we remove the other fruit that was before last seen
                    var notLastSeen = fruitTracker.First(s => s != lastSeen);
                    fruitTracker.Remove(notLastSeen);

                    // now, we track this new fruit
                    lastSeen = fruit;
                    fruitTracker.Add(lastSeen);
                    continuousFruit = 1;
                    currSequence++;
                }
            }
            max = Math.Max(currSequence, max);

            return max;
        }
    }
}

using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class ONSquaredSum : ISimpleChallenge<int, long> {
        public long ExpectedOutput() {
            return 100000L * 100001 / 2;
        }

        public int Input() {
            return 100000;
        }

        public long Run(int input) {
            var sumArray = new long[input];
            for (var i = 1; i <= input; i++) {
                var sum = 0L;
                for (var j = 1; j <= i; j++) {
                    sum += j;
                }
                sumArray[i - 1] = sum;
            }

            return sumArray[input - 1];
        }
    }
}

using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The idea here is that a for the given value, it can be consisted of such subproblem...
    /// What is the minimal amount of coins does it require to make value - coin, where as coin is
    /// a value inside of coins.
    /// So in this case, if we find the minimal of value - coin, then we can find the minimal of value where
    /// really the answer is min(value - coin[n]). This is then a recursive subproblem
    /// The base case is that if value == coin[n], then value = 1
    /// </summary>
    [Challenge(Challenge.Done)]
    class CoinChange : IChallenge<Tuple<int[], int>, int> {
        public int ExpectedOutput() {
            return 3;
        }

        public Tuple<int[], int> Input() {
            return new Tuple<int[], int>(new int[] { 1, 2, 5 }, 11);
        }

        public int Run(Tuple<int[], int> input) {
            var coins = input.Item1;
            var amount = input.Item2;

            if (amount == 0) {
                return 0;
            }

            if (coins.Length == 0) {
                return -1;
            }

            var minWaysForN = new int[amount + 1];

            for (var i = 1; i <= amount; i++) {
                minWaysForN[i] = -1;
            }

            // populating the default
            foreach (var coin in coins) {
                if (coin > amount) {
                    continue;
                }
                minWaysForN[coin] = 1;
            }

            for (var i = 1; i <= amount; i++) {
                var minCoins = -1;
                for (var j = 0; j < coins.Length; j++) {
                    if (i - coins[j] <= 0) {
                        continue;
                    }
                    if (minWaysForN[i - coins[j]] == -1) {
                        continue;
                    }
                    if (minCoins != -1) {
                        minCoins = Math.Min(minCoins, minWaysForN[i - coins[j]] + 1);
                    } else {
                        minCoins = minWaysForN[i - coins[j]] + 1;
                    }
                }
                if (minCoins != -1) {
                    if (minWaysForN[i] != -1) {
                        minWaysForN[i] = Math.Min(minCoins, minWaysForN[i]);
                    } else {
                        minWaysForN[i] = minCoins;
                    }
                }
            }

            return minWaysForN[amount];
        }
    }
}

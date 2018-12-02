using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Basically keep track of the best run previously. If you find a new low, then reset everything as
    /// that can be the new potential buying point
    /// </summary>
    [Challenge(Challenge.Done)]
    class BestTimeToBuyStocks : IChallenge<int[], int> {
        public int ExpectedOutput() {
            return 5;
        }

        public int[] Input() {
            return new int[] { 7, 1, 5, 3, 6, 4 };
        }

        public int Run(int[] input) {
            var prices = input;

            if (prices.Length == 0) {
                return 0;
            }

            var high = prices[0];
            var low = prices[0];
            var bestHigh = high;
            var bestLow = low;

            foreach (var price in prices) {
                if (price < low) {
                    if (bestHigh - bestLow < high - low) {
                        bestHigh = high;
                        bestLow = low;
                    }
                    low = price;
                    high = low;
                }
                if (price > high) {
                    high = price;
                }
            }

            if (bestHigh - bestLow < high - low) {
                bestHigh = high;
                bestLow = low;
            }
            return bestHigh - bestLow;
        }
    }
}

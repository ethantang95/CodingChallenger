using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Divide this into 2 triangles and do cascading multiplication
    /// </summary>
    [Challenge(Challenge.Done)]
    class ProductOfArrayExceptSelf : IChallenge<int[], int[]> {
        public int[] ExpectedOutput() {
            return new int[] { 24, 12, 8, 6 };
        }

        public int[] Input() {
            return new int[] { 1, 2, 3, 4 };
        }

        public int[] Run(int[] input) {
            var nums = input;

            var output = new int[nums.Length];

            for (var i = 0; i < output.Length; i++) {
                output[i] = 1;
            }

            var currentProduct = 1;

            for (var i = nums.Length - 2; i >= 0; i--) {
                currentProduct = nums[i + 1] * currentProduct;
                output[i] = currentProduct;
            }

            currentProduct = 1;

            for (var i = 1; i < nums.Length; i++) {
                currentProduct = nums[i - 1] * currentProduct;
                output[i] = output[i] * currentProduct;
            }

            return output;
        }
    }
}

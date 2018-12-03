using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class TrappingRainWater : ISimpleChallenge<int[], int> {
        public int ExpectedOutput() {
            return 0;
        }

        public int[] Input() {
            return new int[] { 1 };
        }

        public int Run(int[] input) {
            var height = input;

            if (height.Length == 0) {
                return 0;
            }

            // find the max and position of max(s)
            var maxHeight = -1;
            var positions = new List<int>();
            for (var i = 0; i < height.Length; i++) {
                if (maxHeight < height[i]) {
                    maxHeight = height[i];
                    positions = new List<int>() { i };
                } else if (maxHeight == height[i]) {
                    positions.Add(i);
                }
            }

            // now we know where the max height is, everything spreading around it should be monotonically decreasing
            // do the left side first and find the monotonic increasing pattern
            var leftSideIncreases = new List<(int position, int positionHeight)>();
            for (var i = 0; i < positions.First(); i++) {
                if (leftSideIncreases.Count == 0) {
                    leftSideIncreases.Add((i, height[i]));
                } else {
                    if (height[i] >= leftSideIncreases.Last().positionHeight) {
                        leftSideIncreases.Add((i, height[i]));
                    }
                }
            }
            leftSideIncreases.Add((positions.First(), maxHeight));

            var rightSideIncrease = new List<(int position, int positionHeight)>();
            for (var i = height.Length - 1; i > positions.Last(); i--) {
                if (rightSideIncrease.Count == 0) {
                    rightSideIncrease.Add((i, height[i]));
                } else {
                    if (height[i] >= rightSideIncrease.Last().positionHeight) {
                        rightSideIncrease.Add((i, height[i]));
                    }
                }
            }
            rightSideIncrease.Add((positions.Last(), maxHeight));

            // count the water now
            int water = 0;
            for (var i = 0; i < leftSideIncreases.Count - 1; i++) {
                water += CountWater(leftSideIncreases[i], leftSideIncreases[i + 1], false, height);
            }
            for (var i = 0; i < rightSideIncrease.Count - 1; i++) {
                water += CountWater(rightSideIncrease[i], rightSideIncrease[i + 1], true, height);
            }
            for (var i = 0; i < positions.Count - 1; i++) {
                water += CountWater((positions[i], maxHeight), (positions[i + 1], maxHeight), false, height);
            }
            return water;
        }

        private int CountWater((int position, int positionHeight) start, (int position, int positionHeight) end, bool reverse, int[] heights) {
            var water = 0;
            if (reverse) {
                for (var i = start.position - 1; i > end.position; i--) {
                    water += start.positionHeight - heights[i];
                }
            } else {
                for (var i = start.position + 1; i < end.position; i++) {
                    water += start.positionHeight - heights[i];
                }
            }
            return water;
        }
    }
}

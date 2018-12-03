using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// There are many ways to do this by using memory... but without sorting or using additional memory
    /// the other way to see it is that the input can be a graph.
    /// What this then entails us is to basically try to do cycle detection in the graph
    /// 
    /// This can then allow us to deploy the flyod cycle detection aka turtle and rabbit
    /// We already know that there is a cycle, but what flyod cycles can also do is help find
    /// the beginning of the cycle. Basically after where the two pointers intersect, the distance
    /// from the entrance of the cycle from where the pointers are is the same from the beginning at index 0.
    /// With that, we cna then move one of the pointers back to index 0 and go 1 by 1 until the pointers
    /// collide again
    /// </summary>
    [Challenge(Challenge.Done)]
    class FindDuplicate : ISimpleChallenge<int[], int> {
        public int ExpectedOutput() {
            return 2;
        }

        public int[] Input() {
            return new int[] { 1, 3, 4, 2, 2 };
        }

        public int Run(int[] input) {
            var nums = input;

            // use flyod cycle to find the intersection of the duplicates
            var slow = nums[0];
            var fast = nums[nums[0]];

            while (slow != fast) {
                slow = nums[slow];
                fast = nums[nums[fast]];
            }

            // floyd cycle meets, time to step 1 by 1
            slow = nums[0];
            fast = nums[fast];
            while (slow != fast) {
                slow = nums[slow];
                fast = nums[fast];
            }

            // when they meet is where the cycle begins by some wierd mathematical property
            return slow;
        }
    }
}

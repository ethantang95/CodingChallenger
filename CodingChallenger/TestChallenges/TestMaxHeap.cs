using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using CodingChallenger.GenericDataStructures.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.TestChallenges {
    [Challenge(Challenge.NotDone)]
    class TestMaxHeap : ISimpleChallenge<int[], int[]> {
        public int[] ExpectedOutput() {
            return new int[] { 1, 5, 12, 37, 64, 90, 91, 97 };
        }

        public int[] Input() {
            return new int[] { 5, 12, 64, 1, 37, 90, 91, 97 };
        }

        public int[] Run(int[] input) {
            var maxHeap = new MaxHeap<int>(input);
            var heapified = maxHeap.CreateHeap();
            var result = maxHeap.HeapSort();
            return result.ToArray();
        }
    }
}

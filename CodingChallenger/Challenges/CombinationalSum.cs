using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This is probably an NP complete solution, because we have to find all the sums that adds up to the candidate...
    /// The idea of this is to just simulate a stack when adding values into it
    /// </summary>
    [Challenge(Challenge.Done)]
    class CombinationalSum : ISimpleChallenge<CombinationalSumEntry, IList<IList<int>>> {
        public IList<IList<int>> ExpectedOutput() {
            var ans = new List<IList<int>>();
            ans.Add(new List<int>() { 7 });
            ans.Add(new List<int>() { 2, 2, 3 });
            return ans;
        }

        public CombinationalSumEntry Input() {
            var vals = new int[] { 2, 3, 6, 7 };
            var target = 7;
            return new CombinationalSumEntry(vals, target);
        }

        private List<IList<int>> _answers;

        public IList<IList<int>> Run(CombinationalSumEntry input) {
            var candidates = input.Nums;
            var target = input.Target;

            Array.Sort(candidates);

            _answers = new List<IList<int>>();

            FindNext(new Stack<int>(), candidates, 0, target);

            return _answers;
        }

        private void FindNext(Stack<int> chain, int[] candidates, int pointer, int remaining) {

            for (var i = pointer; i < candidates.Length; i++) {
                var value = candidates[i];
                var result = remaining - value;
                chain.Push(value);
                if (result == 0) {
                    _answers.Add(chain.ToList());
                    chain.Pop();
                    return;
                } else if (result > 0) {
                    FindNext(chain, candidates, i, result);
                    chain.Pop();
                } else if (result < 0) {
                    chain.Pop();
                    return;
                }
            }
        }
    }

    class CombinationalSumEntry {
        public int[] Nums { get; private set; }
        public int Target { get; private set; }

        public CombinationalSumEntry(int[] nums, int target) {
            Nums = nums;
            Target = target;
        }
    }
}

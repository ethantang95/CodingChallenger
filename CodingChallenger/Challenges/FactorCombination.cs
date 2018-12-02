using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The idea used here is actually DP, where as solving the factors of a number was already something we solved before
    /// Basically we start with the largest divisor less than the squart root and work our way down
    /// This is prone to duplicates, so therefore a result set must be kept
    /// 
    /// Other way of doing this is working our way up from 2 to sqrt(n), this would ensure no duplicates would occur
    /// </summary>
    [Challenge(Challenge.Done)]
    class FactorCombination : IChallenge<int, IList<IList<int>>> {
        public IList<IList<int>> ExpectedOutput() {
            var result = new List<IList<int>>();
            result.Add(new List<int>(new int[] { 8, 8 }));
            result.Add(new List<int>(new int[] { 4, 16 }));
            result.Add(new List<int>(new int[] { 4, 4, 4 }));
            result.Add(new List<int>(new int[] { 2, 32 }));
            result.Add(new List<int>(new int[] { 2, 4, 8}));
            result.Add(new List<int>(new int[] { 2, 2, 2, 8 }));
            result.Add(new List<int>(new int[] { 2, 2, 2, 2, 4 }));
            result.Add(new List<int>(new int[] { 2, 2, 2, 2, 2, 2 }));
            result.Add(new List<int>(new int[] { 2, 2, 16 }));
            result.Add(new List<int>(new int[] { 2, 2, 4, 4 }));
            return result;
        }

        public int Input() {
            return 64;
        }

        Dictionary<int, List<Tuple<int, int>>> _factorList;
        HashSet<string> _answerSet;
        IList<IList<int>> _answerList;

        public IList<IList<int>> Run(int input) {
            _factorList = new Dictionary<int, List<Tuple<int, int>>>();
            _answerSet = new HashSet<string>();
            _answerList = new List<IList<int>>();
            var max = (int)Math.Sqrt(input);

            CreateAllFactors(input);

            for (var i = max; i > 1; i--) {
                if (input % i == 0) {
                    var divisor = i;
                    var result = input / i;
                    var answer = new List<int>() { divisor, result };
                    _answerSet.Add(string.Join(" ", answer));
                    _answerList.Add(answer);
                    FindFactorsRecursive(divisor, answer);
                }
            }

            return _answerList;
        }

        private void CreateAllFactors(int input) {
            var upper = (int)Math.Sqrt(input);

            for (var i = upper; i > 1; i--) {
                if (input % i == 0) {
                    _factorList.Add(i, new List<Tuple<int, int>>());
                    if (i != input / i) {
                        _factorList.Add(input / i, new List<Tuple<int, int>>());
                    }
                }
            }

            foreach (var factor in _factorList) {
                CreateFactorsPairs(factor.Key);
            }
        }

        private void FindFactorsRecursive(int min, List<int> prev) {
            for (var i = 1; i < prev.Count; i++) {
                var factors = _factorList[prev[i]];
                foreach (var factor in factors) {
                    if (factor.Item1 >= min && factor.Item2 >= min) {
                        var result = ReplaceAndInsert(i, prev, factor);
                        result.Sort();
                        var resultString = string.Join(" ", result);
                        if (!_answerSet.Contains(resultString)) {
                            _answerSet.Add(resultString);
                            _answerList.Add(result);
                            FindFactorsRecursive(min, result);
                        }
                    }
                }
            }
        }

        private List<int> ReplaceAndInsert(int index, List<int> toInsert, Tuple<int, int> values) {
            var frontSubList = toInsert.Take(index).ToList(); ;
            frontSubList.Add(values.Item1);
            frontSubList.Add(values.Item2);
            if (index < toInsert.Count - 1) {
                var backSubList = toInsert.GetRange(index + 1, toInsert.Count - index - 1);
                frontSubList.AddRange(backSubList);
            }
            return frontSubList;
        }

        private void CreateFactorsPairs(int input) {
            var upper = (int)Math.Sqrt(input);

            for (var i = upper; i > 1; i--) {
                if (input % i == 0) {
                    _factorList[input].Add(new Tuple<int, int>(i, input / i));
                }
            }
        }
    }
}

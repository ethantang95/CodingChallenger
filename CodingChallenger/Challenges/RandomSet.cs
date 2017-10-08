using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    public class RandomSet : IChallenge<int, IEnumerable<string>> {

        public IEnumerable<string> ExpectedOutput() {
            return new List<string>(new string[] { "true", "false", "true", "2", "true", "false", "2" });
        }

        public int Input() {
            return 0;
        }

        public IEnumerable<string> Run(int a) {
            var set = new RandomizedSet();
            var result = new List<string>();
            result.Add(set.Insert(1).ToString());
            result.Add(set.Insert(1).ToString());
            result.Add(set.Remove(1).ToString());
            result.Add(set.GetRandom().ToString());
            return result;
        }

        public class RandomizedSet {

            Dictionary<int, int> _randomSet;
            Dictionary<int, int> _inverseRandomSet;
            int _counter;
            Random _random;
            /** Initialize your data structure here. */
            public RandomizedSet() {
                _randomSet = new Dictionary<int, int>();
                _inverseRandomSet = new Dictionary<int, int>();
                _random = new Random();
                _counter = 0;
            }

            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val) {
                if (_inverseRandomSet.ContainsKey(val)) {
                    return false;
                }
                _randomSet.Add(_counter, val);
                _inverseRandomSet.Add(val, _counter);
                _counter++;
                return true;
            }

            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val) {
                if (!_inverseRandomSet.ContainsKey(val)) {
                    return false;
                }
                var randomSetIndex = _inverseRandomSet[val];
                _inverseRandomSet.Remove(val);
                _randomSet.Remove(randomSetIndex);
                if (_counter - 1 != randomSetIndex) {
                    MoveIndex(_counter - 1, randomSetIndex);
                }
                _counter--;
                return true;
            }

            /** Get a random element from the set. */
            public int GetRandom() {
                var index = _random.Next(_counter);
                return _randomSet[index];
            }

            private void MoveIndex(int from, int to) {
                var val = _randomSet[from];
                _randomSet.Remove(from);
                _randomSet.Add(to, val);
                _inverseRandomSet[val] = to;
            }
        }
    }
}

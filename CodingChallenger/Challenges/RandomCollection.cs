﻿using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This is an extension to the Random Set question where now we can have duplicates.
    /// The approach here is to find a way to store the all the indexes for the same element.
    /// The way to do this is to use a HashSet, which allows retrieval and deletion in O(1) time.
    /// This is because we do not care about the order of the elements again, so as long as
    /// We can get a result in O(1) time, it is all fine.
    /// 
    /// Also, the index to element set is still a regular dictionary. This is because even
    /// in the case of duplicates, it is impossible for two elements, whether same or different
    /// to occupy the same index.
    /// </summary>
    [Challenge(Challenge.Done)]
    public class RandomCollection : ISimpleChallenge<int, IEnumerable<string>> {


        public IEnumerable<string> ExpectedOutput() {
            return new List<string>(new string[] { "true", "false", "true", "2", "true", "false", "2" });
        }

        public int Input() {
            return 0;
        }

        public IEnumerable<string> Run(int a) {
            var set = new RandomizedCollection();
            var result = new List<string>();
            result.Add(set.Insert(1).ToString());
            result.Add(set.Insert(1).ToString());
            result.Add(set.Remove(1).ToString());
            result.Add(set.GetRandom().ToString());
            return result;
        }

        public class RandomizedCollection {

            Dictionary<int, int> _randomSet;
            Dictionary<int, HashSet<int>> _inverseRandomSet;
            int _counter;
            Random _random;
            /** Initialize your data structure here. */
            public RandomizedCollection() {
                _randomSet = new Dictionary<int, int>();
                _inverseRandomSet = new Dictionary<int, HashSet<int>>();
                _random = new Random();
                _counter = 0;
            }

            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val) {
                if (_inverseRandomSet.ContainsKey(val)) {
                    _inverseRandomSet[val].Add(_counter);
                    _randomSet.Add(_counter, val);
                    _counter++;
                    return false;
                }
                _randomSet.Add(_counter, val);
                _inverseRandomSet.Add(val, new HashSet<int>());
                _inverseRandomSet[val].Add(_counter);
                _counter++;
                return true;
            }

            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val) {
                if (!_inverseRandomSet.ContainsKey(val)) {
                    return false;
                }
                var randomSetIndex = _inverseRandomSet[val].First();
                _inverseRandomSet[val].Remove(randomSetIndex);
                if (_inverseRandomSet[val].Count == 0) {
                    _inverseRandomSet.Remove(val);
                }
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
                _inverseRandomSet[val].Remove(from);
                _inverseRandomSet[val].Add(to);
            }
        }
    }
}
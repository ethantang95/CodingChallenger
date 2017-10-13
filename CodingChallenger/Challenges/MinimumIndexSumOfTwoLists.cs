using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Not much of a summary to write about this... really it's straight forward
    /// with a hash table to keep track of stuff
    /// </summary>
    [Challenge(Challenge.Done)]
    class MinimumIndexSumOfTwoLists : IChallenge<Tuple<string[], string[]>, string[]> {
        public string[] ExpectedOutput() {
            return new string[] { "Shogun" };
        }

        public Tuple<string[], string[]> Input() {
            var list1 = new string[] { "Shogun", "Tapioca Express", "Burger King", "KFC" };
            var list2 = new string[] { "KFC", "Shogun", "Burger King" };
            return new Tuple<string[], string[]>(list1, list2);
    }

        public string[] Run(Tuple<string[], string[]> input) {
            var list1 = input.Item1;
            var list2 = input.Item2;

            var list1Entries = new Dictionary<string, int>();

            for (var i = 0; i < list1.Length; i++) {
                list1Entries.Add(list1[i], i);
            }

            var minIndex = int.MaxValue;
            var minMembers = new List<string>();

            for (var i = 0; i < list2.Length; i++) {
                if (list1Entries.ContainsKey(list2[i])) {
                    var sum = i + list1Entries[list2[i]];
                    if (sum < minIndex) {
                        minIndex = sum;
                        minMembers = new List<string>();
                        minMembers.Add(list2[i]);
                    } else if (sum == minIndex) {
                        minMembers.Add(list2[i]);
                    }
                }
            }

            return minMembers.ToArray();
        }
    }
}

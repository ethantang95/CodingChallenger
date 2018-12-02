using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This is actually a fairly bad solution with O(n^4) or higher run time. This uses a iterative solution wher ewe know that to build the
    /// next set of brackets, we need the brackets of the previous set.
    /// 
    /// The downside is that this generates a lot of duplicates
    /// </summary>
    [Challenge(Challenge.Done)]
    class GenerateParentheses : IChallenge<int, IList<string>> {
        public IList<string> ExpectedOutput() {
            var result = new List<string>();
            result.Add("()()()");
            result.Add("(())()");
            result.Add("()(())");
            result.Add("(()())");
            result.Add("((()))");
            return result;
        }

        public int Input() {
            return 8;
        }

        public IList<string> Run(int input) {
            if (input == 0) {
                return new List<string>();
            }
            var resultSet = new HashSet<string>();
            resultSet.Add("()");

            for (int i = 2; i <= input; i++) {
                resultSet = GenerateParanthesis(resultSet);
            }

            return resultSet.ToList();
        }

        private HashSet<string> GenerateParanthesis(HashSet<string> prev) {
            var resultSet = new HashSet<string>();

            foreach (var prevEntry in prev) {
                //add left, right, center, and around
                for (var i = 0; i <= prevEntry.Length; i++) {
                    var frontSub = prevEntry.Substring(0, i);
                    var endSub = prevEntry.Substring(i);
                    resultSet.Add(frontSub + "()" + endSub);
                }
            }

            return resultSet;
        }
    }
}

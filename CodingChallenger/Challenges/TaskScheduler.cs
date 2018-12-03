using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Yes it can be a priority queue question which can be done in O(nlogn) but really it can also be a counting question
    /// where the limitation is based on the most frequent task
    /// </summary>
    [Challenge(Challenge.Done)]
    class TaskScheduler : ISimpleChallenge<Tuple<char[], int>, int> {
        public int ExpectedOutput() {
            return 16;
        }

        public Tuple<char[], int> Input() {
            return new Tuple<char[], int>(new char[] { 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, 2);
        }

        public int Run(Tuple<char[], int> input) {
            var tasks = input.Item1;
            var n = input.Item2;

            var tasksCount = new Dictionary<char, int>();
            foreach (var task in tasks) {
                if (!tasksCount.ContainsKey(task)) {
                    tasksCount.Add(task, 0);
                }
                tasksCount[task]++;
            }

            var max = tasksCount.Values.Max();
            var tasksWithMaxCount = tasksCount.Where(s => s.Value == max).Count();
            return Math.Max((((max - 1) * (n + 1)) + tasksWithMaxCount), tasks.Length);
        }
    }
}

using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Something about the official solution's java example is a bit messy... but in this case, we want to sort it in
    /// such a way where the first value for a set where the start are the same are ordered in an increasing manner for
    /// the difference between end and start.
    /// 
    /// Afterwards, we can treat it as an inverse problem, only list down how many we would take... and there are
    /// 2 operations, take, and swap.
    /// 
    /// Take is when we hop onto the next interval because that interval does not overlap with our current one
    /// A swap is when we realize that the next interval is better fit than our current interval, as in they fit
    /// within our current interval and therefore is smaller... since it is smaller, this can potentially fit more
    /// other intervals
    /// </summary>
    [Challenge(Challenge.Done)]
    class NonOverlappingIntervals : IChallenge<NonOverlappingIntervals.Interval[], int> {
        public int ExpectedOutput() {
            return 2;
        }

        public Interval[] Input() {
            var input = new Interval[4];
            input[0] = new Interval(1, 100);
            input[1] = new Interval(11, 22);
            input[2] = new Interval(1, 11);
            input[3] = new Interval(2, 12);
            return input;
        }

        public int Run(Interval[] input) {
            var intervals = input;

            if (intervals.Length == 0) {
                return 0;
            }

            var intervalsSorted = intervals.ToList();
            intervalsSorted.Sort((r, s) => {
                if (r.start == s.start) {
                    return (r.end - r.start) - (s.end - s.start);
                } else {
                    return r.start - s.start;
                }
            });

            var currentInterval = intervalsSorted[0];
            var count = 1;

            foreach (var interval in intervalsSorted.Skip(1)) {
                if (currentInterval.end <= interval.start) {
                    currentInterval = interval;
                    count++;
                } else if (interval.start >= currentInterval.start && interval.end <= currentInterval.end) {
                    currentInterval = interval;
                }
            }
            return intervals.Length - count;
        }

        private List<Interval> GetSmallestIntervals(List<Interval> intervals) {
            var toReturn = new List<Interval>();
            toReturn.Add(intervals[0]);
            var currentStart = intervals[0].start;
            foreach (var interval in intervals) {
                if (interval.start == currentStart) {
                    continue;
                } else {
                    toReturn.Add(interval);
                    currentStart = interval.start;
                }
            }
            return toReturn;
        }

        public class Interval {
            public int start;
            public int end;
            public Interval() { start = 0; end = 0; }
            public Interval(int s, int e) { start = s; end = e; }
        }
    }
}

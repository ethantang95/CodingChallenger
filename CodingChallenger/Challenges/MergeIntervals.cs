using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Sorting first required by the start. Then it is just checking if the start of the next interval is between
    /// the previous's interval. If so, that can be merged.
    /// Merged is just the min of both and the max of both
    /// </summary>
    [Challenge(Challenge.Done)]
    class MergeIntervals : IChallenge<IList<MergeIntervals.Interval>, IList<MergeIntervals.Interval>> {
        public IList<Interval> ExpectedOutput() {
            var intervals = new List<Interval> {
                new Interval(1, 5)
            };
            return intervals;
        }

        public IList<Interval> Input() {
            var intervals = new List<Interval> {
                new Interval(1, 4),
                new Interval(4, 5)
            };
            return intervals;
        }

        public IList<Interval> Run(IList<Interval> input) {
            var intervals = input;

            intervals = intervals.OrderBy(s => s.start).ToList();

            var mergedIntervals = new List<Interval>();
            if (intervals.Count == 0) {
                return mergedIntervals;
            }

            var currentInterval = intervals[0];

            for (var i = 1; i < intervals.Count; i++) {
                var interval = intervals[i];
                if (Overlap(currentInterval, interval)) {
                    var start = Math.Min(currentInterval.start, interval.start);
                    var end = Math.Max(currentInterval.end, interval.end);
                    var newInterval = new Interval(start, end);
                    currentInterval = newInterval;
                } else {
                    mergedIntervals.Add(currentInterval);
                    currentInterval = interval;
                }
            }

            mergedIntervals.Add(currentInterval);

            return mergedIntervals;
        }

        private bool Overlap(Interval a, Interval b) {
            return b.start <= a.end;
        }

        public class Interval {
            public int start;
            public int end;
            public Interval() { start = 0; end = 0; }
            public Interval(int s, int e) { start = s; end = e; }
            public override bool Equals(object obj) {
                var o = obj as Interval;
                return o.start == start && o.end == end;
            }
            public override string ToString() {
                return $"[{start}, {end}]";
            }
        }
    }
}

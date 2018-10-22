using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges
{
    /// <summary>
    /// The trick of this is to know a few things... a valid b has to be one of the following cases:
    /// 1. b is just the string a repeated a few times
    /// 2. b is just the string a repeated a few times plus a prefix that is a suffix of a
    /// 3. b is just the string a repeated a few times plus a suffix that is a prefix of a
    /// 4. a combination of 2 and 3
    /// If it is not one of these cases, then it is impossible to construct a such that b is a substring of
    /// So the length the string to contain b is one of the following
    /// 1. to satisify just case 1 above, containing string is just a repeated len(b) / len(a) times, where this creates an whole integer
    /// 2. to satisify cases 2 and 3, then the containing string is 1 plus 1 more repeated time so that it can contain the suffix or prefix
    /// 3. to satisify case 4, then the containing string is 1 plus 2 more repeated time so that i can contain both the suffix and prefix
    /// These are the only 3 cases to check for, there is no need for anything else
    /// </summary>
    [Challenge(Challenge.Done)]
    class RepeatedStringMatch : IChallenge<Tuple<string, string>, int>
    {
        public int ExpectedOutput()
        {
            return 3;
        }

        public Tuple<string, string> Input()
        {
            return new Tuple<string, string>("abcd", "cdabcdab");
        }

        public int Run(Tuple<string, string> input)
        {
            var a = input.Item1;
            var b = input.Item2;

            // theory 1, the amount of times a must be repeated is either len(b) / len(a), that + 1, and that + 2
            var bTimesGreater = b.Length / a.Length;
            var aStringBuilder = new StringBuilder();
            for (var i = 0; i < bTimesGreater; i++) {
                aStringBuilder.Append(a);
            }

            if (aStringBuilder.ToString().Contains(b)) {
                return bTimesGreater;
            }

            aStringBuilder.Append(a);
            if (aStringBuilder.ToString().Contains(b)) {
                return bTimesGreater + 1;
            }

            aStringBuilder.Append(a);
            if (aStringBuilder.ToString().Contains(b)) {
                return bTimesGreater + 2;
            }

            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Tools {
    class Pair<T1, T2> {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }

    class Pair<T1, T2, T3> {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
    }

    class Pair<T1, T2, T3, T4> {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 ITem4 { get; set; }
    }
}

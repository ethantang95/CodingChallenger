using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework {
    interface IChallenge <TInput, TOutput> {
        TInput Input();
        TOutput ExpectedOutput();
        TOutput Run(TInput input);
    }
}

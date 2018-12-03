using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework.ChallengeInterface {
    interface ISimpleChallenge <TInput, TOutput> : IChallenge {
        TInput Input();
        TOutput ExpectedOutput();
        TOutput Run(TInput input);
    }
}

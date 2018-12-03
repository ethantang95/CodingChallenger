using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework.ChallengeInterface {
    interface IChallengeModifyInput<TInput, TOutput> : ISimpleChallenge<TInput, TOutput> {
        TInput ExpectedModifiedInput();
        bool AssertResult(TInput expected, TInput actual, TOutput output);
    }
}

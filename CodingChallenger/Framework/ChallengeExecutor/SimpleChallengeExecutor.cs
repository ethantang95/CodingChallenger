using CodingChallenger.Framework.ChallengeExecutor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework.ChallengeExecutor {
    class SimpleChallengeExecutor : ChallengeExecutorAbstract {

        public SimpleChallengeExecutor(Type challenge) : base(challenge) { }

        public override void Run() {
            CreateInstance();
            var input = CallMethod("Input");
            var output = CallMethod("Run", input);
            var expectedOutput = CallMethod("ExpectedOutput");
            if (CheckIfEquals(output, expectedOutput)) {
                Console.WriteLine($"Challenge {Challenge.Name} has successfully passed");
            }
        }
    }
}

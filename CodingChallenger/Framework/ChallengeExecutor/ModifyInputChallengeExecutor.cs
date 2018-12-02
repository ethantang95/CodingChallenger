using CodingChallenger.Framework.ChallengeExecutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework {
    class ModifyInputChallengeExecutor : ChallengeExecutorAbstract{

        public ModifyInputChallengeExecutor(Type challenge) : base(challenge) { }

        public override void Run() {
            var input = CallMethod("Input");
            var output = CallMethod("Run", input);
            var expectedOutput = CallMethod("ExpectedOutput");
            CheckIfEquals(output, expectedOutput);
            var expectedModifiedInput = CallMethod("ExpectedModifiedInput");
            var assertResult = (bool)CallMethod("AssertResult", expectedModifiedInput, input, output);

            if (!assertResult) {
                CheckIfEquals(expectedModifiedInput, input);
                Console.WriteLine($"Challenge {Challenge.Name} did not pass, please debug");
            } else {
                Console.WriteLine($"Challenge {Challenge.Name} has successfully passed");
            }
            Console.ReadLine();
        }
    }
}

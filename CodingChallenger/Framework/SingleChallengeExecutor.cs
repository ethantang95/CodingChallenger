using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework {
    class SingleChallengeExecutor {
        private Type _challenge;
        private object _instance;

        public SingleChallengeExecutor(Type challenge) {
            _challenge = challenge;
        }

        public void Run() {
            CreateInstance();
            var input = GetInput();
            var output = GetRunResult(input);
            var expectedOutput = GetExpectedOutput();
            CheckIfEquals(output, expectedOutput);
        }

        private void CreateInstance() {
            var obj = Activator.CreateInstance(_challenge);
            _instance = obj;
        }

        private object GetInput() {
            var inputMethod = _challenge.GetMethod("Input");
            var input = inputMethod.Invoke(_instance, new object[] { });
            return input;
        }

        private object GetRunResult(object input) {
            var runMethod = _challenge.GetMethod("Run");
            var runResult = runMethod.Invoke(_instance, new object[] { input });
            return runResult;
        }

        private object GetExpectedOutput() {
            var expectedOutputMethod = _challenge.GetMethod("ExpectedOutput");
            var expectedOutput = expectedOutputMethod.Invoke(_instance, new object[] { });
            return expectedOutput;
        }

        private void CheckIfEquals(object output, object expectedOutput) {
            if (!expectedOutput.Equals(output)) {
                Console.WriteLine($"output and expected output are not the same for challenge {_challenge.Name}, please debug");
            } else {
                Console.WriteLine($"Challenge {_challenge.Name} has successfully passed");
            }
            Console.ReadLine();
        }
    }
}

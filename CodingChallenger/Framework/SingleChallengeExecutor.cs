using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
            try {
                var inputMethod = _challenge.GetMethod("Input");
                var input = inputMethod.Invoke(_instance, new object[] { });
                return input;
            } catch (Exception e) {
                ThrowDynamicException(e);
                return null;
            }
        }

        private object GetRunResult(object input) {
            try {
                var runMethod = _challenge.GetMethod("Run");
                var runResult = runMethod.Invoke(_instance, new object[] { input });
                return runResult;
            } catch (Exception e) {
                ThrowDynamicException(e);
                return null;
            }
        }

        private object GetExpectedOutput() {
            try {
                var expectedOutputMethod = _challenge.GetMethod("ExpectedOutput");
                var expectedOutput = expectedOutputMethod.Invoke(_instance, new object[] { });
                return expectedOutput;
            } catch (Exception e) {
                ThrowDynamicException(e);
                return null;
            }
        }

        private void CheckIfEquals(object output, object expectedOutput) {
            if (output is IEnumerable<int>) {
                OutputEnumerables(output as IEnumerable<int>, expectedOutput as IEnumerable<int>);
            } else if (output is IEnumerable<object>) {
                OutputEnumerables(output as IEnumerable<object>, expectedOutput as IEnumerable<object>);
            } else {
                if (!expectedOutput.Equals(output)) {
                    Console.WriteLine($"output and expected output are not the same for challenge {_challenge.Name}, please debug");
                    Console.WriteLine($"output is {output}, expected output is {expectedOutput}");
                } else {
                    Console.WriteLine($"Challenge {_challenge.Name} has successfully passed");
                }
            }
            Console.ReadLine();
        }

        private void OutputEnumerables<T>(IEnumerable<T> output, IEnumerable<T> expectedOutput) {
            foreach (var entries in output.Zip(expectedOutput, Tuple.Create)) {
                var outputEntry = entries.Item1;
                var expectedOutputEntry = entries.Item2;
                if (!expectedOutputEntry.Equals(outputEntry)) {
                    Console.WriteLine($"output and expected output are not the same for challenge {_challenge.Name}, please debug");
                    Console.WriteLine("outputs are:");
                    foreach (var entry in output as IEnumerable) {
                        Console.WriteLine(entry.ToString());
                    }
                    Console.WriteLine("Expected outputs are:");
                    foreach (var entry in expectedOutput as IEnumerable) {
                        Console.WriteLine(entry.ToString());
                    }
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine($"Challenge {_challenge.Name} has successfully passed");
        }

        private void ThrowDynamicException(Exception e) {
            var capturedException = ExceptionDispatchInfo.Capture(e);
            capturedException.Throw();
        }
    }
}

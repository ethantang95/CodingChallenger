using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework.ChallengeExecutor {
    abstract class ChallengeExecutorAbstract {

        protected Type Challenge { get; private set; }
        protected object Instance { get; private set; }

        public ChallengeExecutorAbstract(Type challenge) {
            Challenge = challenge;
            CreateInstance();
        }

        public abstract void Run();

        protected void CreateInstance() {
            var obj = Activator.CreateInstance(Challenge);
            Instance = obj;
        }

        protected object CallMethod(string method, params object[] args) {
            try {
                var runMethod = Challenge.GetMethod(method);
                var runResult = runMethod.Invoke(Instance, args);
                return runResult;
            } catch (Exception e) {
                ThrowDynamicException(e);
                return null;
            }
        }

        protected bool CheckIfEquals(object output, object expectedOutput) {
            if (output is IEnumerable<int>) {
                return CheckEnumerables(output as IEnumerable<int>, expectedOutput as IEnumerable<int>);
            } else if (output is IEnumerable<char>) {
                return CheckEnumerables(output as IEnumerable<char>, expectedOutput as IEnumerable<char>);
            } else if (output is IEnumerable<double>) {
                return CheckEnumerables(output as IEnumerable<double>, expectedOutput as IEnumerable<double>);
            } else if (output is IEnumerable<object>) {
                return CheckEnumerables(output as IEnumerable<object>, expectedOutput as IEnumerable<object>);
            } else {
                if (!expectedOutput.Equals(output)) {
                    Console.WriteLine($"output and expected output are not the same for challenge {Challenge.Name}, please debug");
                    Console.WriteLine($"output is {output}, expected output is {expectedOutput}");
                    return false;
                } else {
                    return true;
                }
            }
        }

        private bool CheckEnumerables<T>(IEnumerable<T> output, IEnumerable<T> expectedOutput) {
            if (output.Count() != expectedOutput.Count()) {
                PrintBadEnumerableOutput(output, expectedOutput);
                return false;
            }
            foreach (var entries in output.Zip(expectedOutput, Tuple.Create)) {
                var outputEntry = entries.Item1;
                var expectedOutputEntry = entries.Item2;
                if (!expectedOutputEntry.Equals(outputEntry)) {
                    PrintBadEnumerableOutput(output, expectedOutput);
                    return false; 
                }
            }
            return true;
        }

        private void PrintBadEnumerableOutput<T>(IEnumerable<T> output, IEnumerable<T> expectedOutput) {
            Console.WriteLine($"output and expected output are not the same for challenge {Challenge.Name}, please debug");
            Console.WriteLine("outputs are:");
            foreach (var entry in output as IEnumerable) {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("Expected outputs are:");
            foreach (var entry in expectedOutput as IEnumerable) {
                Console.WriteLine(entry.ToString());
            }
            return;
        }

        private void ThrowDynamicException(Exception e) {
            var capturedException = ExceptionDispatchInfo.Capture(e);
            capturedException.Throw();
        }
    }
}

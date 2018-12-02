using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Very simple, use a stack for this
    /// </summary>
    [Challenge(Challenge.Done)]
    class BaseballGame : IChallenge<string[], int> {
        public int ExpectedOutput() {
            return 27;
        }

        public string[] Input() {
            return new string[] { "5", "-2", "4", "C", "D", "9", "+", "+" };
        }

        public int Run(string[] input) {
            var pointStack = new Stack<int>();

            foreach (var entry in input) {
                if (entry == "C") {
                    pointStack.Pop();
                } else if (entry == "D") {
                    var lastPt = pointStack.Peek();
                    pointStack.Push(lastPt * 2);
                } else if (entry == "+") {
                    var lastPt1 = pointStack.Pop();
                    var lastPt2 = pointStack.Peek();
                    pointStack.Push(lastPt1);
                    pointStack.Push(lastPt1 + lastPt2);
                } else {
                    var pt = int.Parse(entry);
                    pointStack.Push(pt);
                }
            }

            return pointStack.Sum();
        }
    }
}

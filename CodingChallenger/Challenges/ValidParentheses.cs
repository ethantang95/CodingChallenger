using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Basically a problem using stacks. I used the ascii table as a way to not write too much if and else statements
    /// </summary>
    [Challenge(Challenge.Done)]
    class ValidParentheses : ISimpleChallenge<string, bool> {
        public bool ExpectedOutput() {
            return false;
        }

        public string Input() {
            return "([]){";
        }

        public bool Run(string input) {
            var braceStack = new Stack<char>();

            foreach (var c in input) {
                if (braceStack.Count == 0) {
                    braceStack.Push(c);
                } else if (c == braceStack.Peek()) {
                    braceStack.Push(c);
                } else if (c == 40 || c == 91 || c == 123) {
                    braceStack.Push(c);
                } else if (Math.Abs(c - braceStack.Peek()) < 3) {
                    braceStack.Pop();
                } else {
                    return false;
                }
            }

            return braceStack.Count == 0;
        }
    }
}

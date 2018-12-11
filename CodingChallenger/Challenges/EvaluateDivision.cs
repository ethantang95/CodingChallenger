using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.NotDone)]
    class EvaluateDivision : ISimpleChallenge<(string[,] equations, double[] values, string[,] queries), double[]> {
        public double[] ExpectedOutput() {
            return new double[] { 6.0, 0.5, -1.0, 1.0, -1.0 };
        }

        public (string[,] equations, double[] values, string[,] queries) Input() {
            var equations = new string[,] { { "a", "b" }, { "b", "c" } };
            var values = new double[] { 2.0, 3.0 };
            var queries = new string[,] { { "a", "c" }, { "b", "a" }, { "a", "e" }, { "a", "a" }, { "x", "x" } };
            return (equations, values, queries);
        }

        public double[] Run((string[,] equations, double[] values, string[,] queries) input) {
            var equations = input.equations;
            var values = input.values;
            var queries = input.queries;

            // first making a dictionary mapping the value and to which ever node
            var equationMappings = new Dictionary<string, List<(string node, double val)>>();
            for (var i = 0; i < values.Length; i++) {
                var dividend = equations[i, 0];
                var divisor = equations[i, 1];
                if (!equationMappings.ContainsKey(dividend)) {
                    equationMappings.Add(dividend, new List<(string node, double val)>());
                }
                equationMappings[dividend].Add((divisor, values[i]));
                if (!equationMappings.ContainsKey(divisor)) {
                    equationMappings.Add(divisor, new List<(string node, double val)>());
                }
                equationMappings[divisor].Add((dividend, 1 / values[i]));
;           }

            var answers = new double[queries.GetLength(0)];

            for (var i = 0; i < queries.GetLength(0); i++) {
                answers[i] = FindQuotient(queries[i, 0], queries[i, 1], equationMappings);
            }

            return answers;
        }

        public double FindQuotient(string start, string destination, Dictionary<string, List<(string node, double val)>> graph) {
            if (!(graph.ContainsKey(start) && graph.ContainsKey(destination))) {
                return -1;
            }
            if (start == destination) {
                return 1;
            }
            var (quotient, _) = TraverseGraph(start, destination, graph, new HashSet<string>(), 1);

            return quotient;
        }

        public (double, bool) TraverseGraph(string current, string destination, Dictionary<string, List<(string node, double val)>> graph, HashSet<string> traverseStack, double quotient) {
            var links = graph[current];
            traverseStack.Add(current);
            foreach (var (node, val) in links) {
                if (node == destination) {
                    return (quotient * val, true);
                }
                if (traverseStack.Contains(node)) {
                    continue;
                }
                var (resultQuotient, result) = TraverseGraph(node, destination, graph, traverseStack, quotient * val);
                if (result) {
                    return (resultQuotient, true);
                }
            }
            traverseStack.Remove(current);
            return (-1, false);
        }
    }
}

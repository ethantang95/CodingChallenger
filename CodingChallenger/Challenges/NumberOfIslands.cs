using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Basically just DFS or BFS traverse through the grid. First find a spot where you haven't explored yet and it
    /// is a piece of land, then DFS or BFS around it.
    /// 
    /// For some reason, BFS in C# is a lot slower than DFS where leetcode will time out. That I don't know why
    /// </summary>
    [Challenge(Challenge.Done)]
    class NumberOfIslands : IChallenge<char[,], int> {
        public int ExpectedOutput() {
            return 1;
        }

        public char[,] Input() {
            return new char[3, 3] { { '1', '1', '1' }, { '0', '1', '0' }, { '1', '1', '1' } };
        }

        public int Run(char[,] input) {
            var grid = input;
            var islands = 0;

            for (var i = 0; i < grid.GetLength(0); i++) {
                for (var j = 0; j < grid.GetLength(1); j++) {
                    if (grid[i, j] == '1') {
                        MarkIsland(grid, i, j);
                        islands++;
                    }
                }
            }

            return islands;
        }

        public void MarkIsland(char[,] grid, int i, int j) {
            // We will use DFS to traverse through the islands
            var dfsStack = new Stack<Tuple<int, int>>();
            dfsStack.Push(new Tuple<int, int>(i, j));
            while (dfsStack.Count > 0) {
                var location = dfsStack.Pop();
                grid[location.Item1, location.Item2] = 'x';
                // check the 4 locations
                // up
                var iUp = location.Item1 - 1;
                var jUp = location.Item2;
                if (iUp >= 0 && grid[iUp, jUp] == '1') {
                    dfsStack.Push(new Tuple<int, int>(iUp, jUp));
                }
                // left
                var iLeft = location.Item1;
                var jLeft = location.Item2 - 1;
                if (jLeft >= 0 && grid[iLeft, jLeft] == '1') {
                    dfsStack.Push(new Tuple<int, int>(iLeft, jLeft));
                }
                // right
                var iDown = location.Item1 + 1;
                var jDown = location.Item2;
                if (iDown < grid.GetLength(0) && grid[iDown, jDown] == '1') {
                    dfsStack.Push(new Tuple<int, int>(iDown, jDown));
                }
                // down
                var iRight = location.Item1;
                var jRight = location.Item2 + 1;
                if (jRight < grid.GetLength(1) && grid[iRight, jRight] == '1') {
                    dfsStack.Push(new Tuple<int, int>(iRight, jRight));
                }
            }
        }
    }
}

using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// This doesn't iterate through the entire board, it only keep track of
    /// areas that have live cells and the area surrounding them...
    /// </summary>
    [Challenge(Challenge.Done)]
    class GameOfLife : IChallengeModifyInput<int[][], int> {
        public bool AssertResult(int[][] expected, int[][] actual, int output) {
            for (var i = 0; i < expected.Length; i++) {
                for (var j = 0; j < expected[0].Length; j++) {
                    if (expected[i][j] != actual[i][j]) {
                        return false;
                    }
                }
            }

            return true;
        }

        public int[][] ExpectedModifiedInput() {
            var input = new int[4][];
            input[0] = new int[] { 0, 0, 0 };
            input[1] = new int[] { 1, 0, 1 };
            input[2] = new int[] { 0, 1, 1 };
            input[3] = new int[] { 0, 1, 0 };
            return input;

        }

        public int ExpectedOutput() {
            return 1;
        }

        public int[][] Input() {
            var input = new int[4][];
            input[0] = new int[] { 0, 1, 0 };
            input[1] = new int[] { 0, 0, 1 };
            input[2] = new int[] { 1, 1, 1 };
            input[3] = new int[] { 0, 0, 0 };
            return input;
        }

        public int Run(int[][] input) {
            var board = input;

            var liveCells = FindLiveCells(board);
            var activities = FindActivityTiles(liveCells, board[0].Length, board.Length);
            var updates = ProcessUpdates(activities, board);
            foreach (var (y, x, update) in updates) {
                board[y][x] = update;
            }

            return 1;
        }

        private List<(int y, int x)> FindLiveCells(int[][] board) {
            var liveCells = new List<(int y, int x)>();
            for (var y = 0; y < board.Length; y++) {
                for (var x = 0; x < board[y].Length; x++) {
                    if (board[y][x] == 1) {
                        liveCells.Add((y, x));
                    }
                }
            }
            return liveCells;
        }

        private HashSet<(int y, int x)> FindActivityTiles(List<(int y, int x)> liveCells, int boardWidth, int boardHeight) {
            var widthBound = boardWidth - 1;
            var heightBound = boardHeight - 1;
            var activityTiles = new HashSet<(int y, int x)>();
            foreach (var (y, x) in liveCells) {
                if (y != 0 && x != 0) {
                    activityTiles.Add((y - 1, x - 1));
                }
                if (y != 0) {
                    activityTiles.Add((y - 1, x));
                }
                if (y != 0 && x != widthBound) {
                    activityTiles.Add((y - 1, x + 1));
                }
                if (x != 0) {
                    activityTiles.Add((y, x - 1));
                }
                activityTiles.Add((y, x));
                if (x != widthBound) {
                    activityTiles.Add((y, x + 1));
                }
                if (y != heightBound && x != 0) {
                    activityTiles.Add((y + 1, x - 1));
                }
                if (y != heightBound) {
                    activityTiles.Add((y + 1, x));
                }
                if (y != heightBound && x != widthBound) {
                    activityTiles.Add((y + 1, x + 1));
                }
            }
            return activityTiles;
        }

        private List<(int y, int x, int update)> ProcessUpdates(HashSet<(int y, int x)> activityTiles, int[][] board) {
            var widthBound = board[0].Length - 1;
            var heightBound = board.Length - 1;
            var updates = new List<(int y, int x, int update)>();
            foreach (var (y, x) in activityTiles) {
                int liveNeighbors = 0;
                if (y != 0 && x != 0) {
                    if (board[y - 1][x - 1] == 1) {
                        liveNeighbors++;
                    }
                }
                if (y != 0) {
                    if (board[y - 1][x] == 1) {
                        liveNeighbors++;
                    }
                }
                if (y != 0 && x != widthBound) {
                    if (board[y - 1][x + 1] == 1) {
                        liveNeighbors++;
                    }
                }
                if (x != 0) {
                    if (board[y][x - 1] == 1) {
                        liveNeighbors++;
                    }
                }
                if (x != widthBound) {
                    if (board[y][x + 1] == 1) {
                        liveNeighbors++;
                    }
                }
                if (y != heightBound && x != 0) {
                    if (board[y + 1][x - 1] == 1) {
                        liveNeighbors++;
                    }
                }
                if (y != heightBound) {
                    if (board[y + 1][x] == 1) {
                        liveNeighbors++;
                    }
                }
                if (y != heightBound && x != widthBound) {
                    if (board[y + 1][x + 1] == 1) {
                        liveNeighbors++;
                    }
                }

                if (board[y][x] == 0) {
                    if (liveNeighbors == 3) {
                        updates.Add((y, x, 1));
                    }
                } else {
                    if (!(liveNeighbors == 2 || liveNeighbors == 3)) {
                        updates.Add((y, x, 0));
                    }
                }
            }

            return updates;
        }
    }
}

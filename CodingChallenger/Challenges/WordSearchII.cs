using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using CodingChallenger.GenericDataStructures.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The idea of this is to basically use a trie to sort out the words... as in we go with the prefix until we hit an actual word
    /// And finally add that word to the list we see. This also helps with terminating searches if the word does not exist
    /// Next to that, it is just using DFS search with a trie keeping track of what to search for.
    /// </summary>
    [Challenge(Challenge.Done)]
    class WordSearchII : ISimpleChallenge<Tuple<char[,], string[]>, List<string>> {
        public List<string> ExpectedOutput() {
            return new List<string> { "aaa", "aaab", "aaba", "aba", "baa" };
        }

        public Tuple<char[,], string[]> Input() {
            var board = new char[2, 2] {
                { 'a', 'b' },
                { 'a', 'a' }
            };

            var words = new string[7] { "aba", "baa", "bab", "aaab", "aaa", "aaaa", "aaba" };
            return new Tuple<char[,], string[]>(board, words);
        }

        public List<string> Run(Tuple<char[,], string[]> input) {
            var board = input.Item1;
            var words = input.Item2;

            var rows = board.GetLength(0);
            var columns = board.GetLength(1);
            var trie = new TrieNodeMap<string>();
            var foundWords = new HashSet<string>();
            foreach (var word in words) {
                trie.Add(word, word);
            }

            for (var x = 0; x < columns; x++) {
                for (var y = 0; y < rows; y++) {
                    var c = board[y, x];
                    if (trie[c] != null) {
                        var visitedTiles = new HashSet<Tuple<int, int>>();
                        findWord(x, y, "" + c, trie[c], board, visitedTiles, foundWords);
                    }
                }
            }

            var foundWordSorted = foundWords.ToList();
            foundWordSorted.Sort();
            return foundWordSorted;
        }

        private void findWord(int x, int y, string prefix, TrieNodeMap<string> trieNode, char[,] board, HashSet<Tuple<int, int>> visitedTiles, HashSet<string> foundWords) {
            visitedTiles.Add(new Tuple<int, int>(y, x));
            if (trieNode.Value != null) {
                foundWords.Add(trieNode.Value);
            }
            var rows = board.GetLength(0);
            var columns = board.GetLength(1);

            //check up
            if ((y - 1) >= 0 && !visitedTiles.Contains(new Tuple<int, int>(y - 1, x)) && trieNode[board[y - 1, x]] != null) {
                var c = board[y - 1, x];
                findWord(x, y - 1, prefix + c, trieNode[c], board, visitedTiles, foundWords);
            }
            // check left
            if ((x + 1) < columns && !visitedTiles.Contains(new Tuple<int, int>(y, x + 1)) && trieNode[board[y, x + 1]] != null) {
                var c = board[y, x + 1];
                findWord(x + 1, y, prefix + c, trieNode[c], board, visitedTiles, foundWords);
            }
            // check down
            if ((y + 1) < rows && !visitedTiles.Contains(new Tuple<int, int>(y + 1, x)) && trieNode[board[y + 1, x]] != null) {
                var c = board[y + 1, x];
                findWord(x, y + 1, prefix + c, trieNode[c], board, visitedTiles, foundWords);
            }
            // check right
            if ((x - 1) >= 0 && !visitedTiles.Contains(new Tuple<int, int>(y, x - 1)) && trieNode[board[y, x - 1]] != null) {
                var c = board[y, x - 1];
                findWord(x - 1, y, prefix + c, trieNode[c], board, visitedTiles, foundWords);
            }
            visitedTiles.Remove(new Tuple<int, int>(y, x));
        }
    }
}

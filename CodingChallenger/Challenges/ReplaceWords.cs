using CodingChallenger.Framework;
using CodingChallenger.GenericDataStructures.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Use a trie... this is a very simple prefix trie problem
    /// </summary>
    [Challenge(Challenge.Done)]
    class ReplaceWords : IChallenge<StringAndWordList, string> {
        public string ExpectedOutput() {
            return "the cat was rat by the bat";
        }

        public StringAndWordList Input() {
            var dict = new List<string>() { "cat", "bat", "rat" };
            var sentence = "the cattle was rattled by the battery";
            return new StringAndWordList(dict, sentence);
        }

        public string Run(StringAndWordList input) {
            var dict = input.Dict;
            var sentence = input.Sentence;

            var trie = new TrieNode<string>();
            foreach (var entry in dict) {
                trie.Add(entry, entry);
            }

            var sentenceWords = sentence.Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToList();
            var newSentence = new StringBuilder();

            foreach (var word in sentenceWords) {
                if (trie.Contains(word)) {
                    newSentence.Append(trie.Get(word));
                } else {
                    newSentence.Append(word);
                }
                newSentence.Append(' ');
            }

            return newSentence.ToString().Trim();
        }
    }

    class StringAndWordList {
        public IList<string> Dict { get; set; }
        public string Sentence { get; set; }

        public StringAndWordList(IList<string> dict, string sentence) {
            Dict = dict;
            Sentence = sentence;
        }
    }
}

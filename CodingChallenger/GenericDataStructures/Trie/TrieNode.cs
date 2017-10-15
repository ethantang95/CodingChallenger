using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Trie {
    class TrieNode<T> {
        public T Value { get; private set; }
        public TrieNode<T> this[char c] {
            get {
                if (!char.IsLetter(c)) {
                    throw new ArgumentException("A trie accessor must be a letter between a-z");
                }

                c = char.ToLower(c);

                return _children[c - 97];
            }
        }
        public int TotalCount { get { return _count.Sum(); } }

        public TrieNode<T>[] _children;
        private int[] _count;

        public TrieNode() {
            Value = default(T);
            _children = new TrieNode<T>[26];
        }

        public void Add(string key, T value) {
            if (string.IsNullOrEmpty(key)) {
                if (Value == null) {
                    Value = value;
                } else {
                    throw new ArgumentException($"Key already exist for value {value.ToString()}");
                }
            } else {
                var c = key[0];
                if (!char.IsLetter(c)) {
                    throw new ArgumentException($"Unexpected character {c} from key, the key must all be letters from a-z");
                }

                c = char.ToLower(c);

                if (this[c] == null) {
                    _children[c - 97] = new TrieNode<T>();
                }
                _count[c - 97]++;

                this[c].Add(key.Substring(1), value);
            }
        }

        public int GetCountForLetter(char c) {
            if (!char.IsLetter(c)) {
                throw new ArgumentException("A trie accessor must be a letter between a-z");
            }

            c = char.ToLower(c);

            return _count[c - 97];
        }

        public T Get(string key) {
            if (string.IsNullOrEmpty(key)) {
                return Value;
            } else {
                var c = key[0];
                if (!char.IsLetter(c)) {
                    throw new ArgumentException($"Unexpected character {c} from key, the key must all be letters from a-z");
                }

                c = char.ToLower(c);

                if (this[c] == null) {
                    return default(T);
                }

                return this[c].Get(key.Substring(1));
            }
        }

        public bool Contains(string key) {
            if (string.IsNullOrEmpty(key)) {
                return Value != null;
            } else {
                var c = key[0];
                if (!char.IsLetter(c)) {
                    throw new ArgumentException($"Unexpected character {c} from key, the key must all be letters from a-z");
                }

                c = char.ToLower(c);

                if (this[c] == null) {
                    return false;
                }

                return this[c].Contains(key.Substring(1));
            }
        }
    }
}

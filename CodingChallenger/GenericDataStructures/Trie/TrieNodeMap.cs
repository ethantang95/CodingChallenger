using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Trie {
    class TrieNodeMap<T> where T: class {
        public T Value { get; private set; }
        public TrieNodeMap<T> this[char c] {
            get {
                c = GetFirstLetterLower(c.ToString());
                return _children[c - 97];
            }
        }
        public int TotalCount { get { return _count.Sum(); } }

        public TrieNodeMap<T>[] _children;
        private int[] _count;

        public TrieNodeMap() {
            Value = null;
            _children = new TrieNodeMap<T>[26];
            _count = new int[26];
        }

        public void Add(string key, T value) {
            if (string.IsNullOrEmpty(key)) {
                if (Value == null) {
                    Value = value;
                } else {
                    throw new ArgumentException($"Key already exist for value {value.ToString()}");
                }
            } else {
                var c = GetFirstLetterLower(key);

                if (this[c] == null) {
                    _children[c - 97] = new TrieNodeMap<T>();
                }
                _count[c - 97]++;

                this[c].Add(key.Substring(1), value);
            }
        }

        public int GetCountForLetter(char c) {
            c = GetFirstLetterLower(c.ToString());
            return _count[c - 97];
        }

        public T Get(string key) {
            if (Value != null) {
                return Value;
            } else if (string.IsNullOrEmpty(key)) {
                return null;
            } else {
                var c = GetFirstLetterLower(key);

                if (this[c] == null) {
                    return null;
                }

                return this[c].Get(key.Substring(1));
            }
        }

        public T GetExact(string key) {
            if (string.IsNullOrEmpty(key)) {
                return Value;
            } else {
                var c = GetFirstLetterLower(key);

                if (this[c] == null) {
                    return null;
                }

                return this[c].Get(key.Substring(1));
            }
        }

        public IEnumerable<TrieNodeMap<T>> GetNodes() {
            return _children.Where(s => s != null);
        }

        public bool Contains(string key) {
            if (Value != null) {
                return true;
            } else if (string.IsNullOrEmpty(key)) {
                return false;
            } else {
                var c = GetFirstLetterLower(key);

                if (this[c] == null) {
                    return false;
                }

                return this[c].Contains(key.Substring(1));
            }
        }

        public bool ContainsExact(string key) {
            if (string.IsNullOrEmpty(key)) {
                return Value == null;
            } else {
                var c = GetFirstLetterLower(key);

                if (this[c] == null) {
                    return false;
                }

                return this[c].ContainsExact(key.Substring(1));
            }
        }

        private char GetFirstLetterLower(string s) {
            var c = s[0];
            if (!char.IsLetter(c)) {
                throw new ArgumentException($"Unexpected character {c} from key, the key must all be letters from a-z");
            }

            return char.ToLower(c);
        }
    }
}

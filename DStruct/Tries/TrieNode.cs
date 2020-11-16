using System;
using System.Collections.Generic;

namespace DStruct.Tries
{
    class TrieNode
    {
        private readonly SortedDictionary<char, TrieNode> _dictionary = new SortedDictionary<char, TrieNode>();

        private bool   _endOfWord;
        private string _value;

        private bool IsEmpty => _dictionary.Count == 0 && !_endOfWord;

        public void Clear()
        {
            _dictionary.Clear();
        }

        public List<string> GetAllValues()
        {
            var list = new List<string>();

            if (_endOfWord)
            {
                list.Add(_value);
            }

            foreach (var trieNode in _dictionary)
            {
                list.AddRange(trieNode.Value.GetAllValues());
            }

            return list;
        }

        public bool Add(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("Argument string cannot be null or empty.");
            }

            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    var next = new TrieNode();
                    current._dictionary.Add(c, next);
                    current = next;
                }
            }

            if (current._endOfWord)
                return false;

            current._endOfWord = true;
            current._value     = s;
            return true;
        }

        public bool Remove(string s, int i = 0)
        {
            bool ret = false;

            if (i < s.Length)
            {
                if (_dictionary.ContainsKey(s[i]))
                {
                    ret = _dictionary[s[i]].Remove(s, i + 1);
                }

                if (!ret)
                    return false;
            }

            if (i == s.Length)
            {
                if (!_endOfWord)
                    return false;

                _endOfWord = false;
                _value     = string.Empty;

                return true;
            }

            if (_dictionary[s[i]].IsEmpty)
            {
                _dictionary.Remove(s[i]);
            }

            return true;
        }

        public List<string> GetWithPrefix(string s)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return new List<string>();
                }
            }

            return current.GetAllValues();
        }

        public bool Contains(string s, bool prefix = false)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return false;
                }
            }

            return prefix || current._endOfWord;
        }
    }

    class TrieNode<T> where T : IEquatable<T>
    {
        private readonly SortedDictionary<char, TrieNode<T>> _dictionary = new SortedDictionary<char, TrieNode<T>>();

        private bool   _endOfWord;
        private string _key;
        private T      _value;

        private bool IsEmpty => _dictionary.Count == 0 && !_endOfWord;

        public void Clear()
        {
            _dictionary.Clear();
        }

        public List<KeyValuePair<string, T>> GetAllPairs()
        {
            var list = new List<KeyValuePair<string, T>>();

            if (_endOfWord)
            {
                list.Add(new KeyValuePair<string, T>(_key, _value));
            }

            foreach (var trieNode in _dictionary)
            {
                list.AddRange(trieNode.Value.GetAllPairs());
            }

            return list;
        }

        public List<string> GetAllKeys()
        {
            var list = new List<string>();

            if (_endOfWord)
            {
                list.Add(_key);
            }

            foreach (var trieNode in _dictionary)
            {
                list.AddRange(trieNode.Value.GetAllKeys());
            }

            return list;
        }

        public List<T> GetAllValues()
        {
            var list = new List<T>();

            if (_endOfWord)
            {
                list.Add(_value);
            }

            foreach (var trieNode in _dictionary)
            {
                list.AddRange(trieNode.Value.GetAllValues());
            }

            return list;
        }

        public bool Add(string s, T value)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("Argument string cannot be null or empty.");
            }

            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    var next = new TrieNode<T>();
                    current._dictionary.Add(c, next);
                    current = next;
                }
            }

            current._value = value;
            current._key   = s;

            if (current._endOfWord)
                return false;

            current._endOfWord = true;
            return true;
        }

        public bool Remove(string s, int i = 0)
        {
            bool ret = false;

            if (i < s.Length)
            {
                if (_dictionary.ContainsKey(s[i]))
                {
                    ret = _dictionary[s[i]].Remove(s, i + 1);
                }

                if (!ret)
                    return false;
            }

            if (i == s.Length)
            {
                if (!_endOfWord)
                    return false;

                _endOfWord = false;
                _value     = default;
                _key       = null;

                return true;
            }

            if (_dictionary[s[i]].IsEmpty)
            {
                _dictionary.Remove(s[i]);
            }

            return true;
        }

        public bool Remove(string s, T value, int i = 0)
        {
            bool ret = false;

            if (i < s.Length)
            {
                if (_dictionary.ContainsKey(s[i]))
                {
                    ret = _dictionary[s[i]].Remove(s, value, i + 1);
                }

                if (!ret)
                    return false;
            }

            if (i == s.Length)
            {
                if (!_endOfWord ||
                    !_value.Equals(value))
                    return false;

                _endOfWord = false;
                _value     = default;
                _key       = null;

                return true;
            }

            if (_dictionary[s[i]].IsEmpty)
            {
                _dictionary.Remove(s[i]);
            }

            return true;
        }

        public List<T> GetWithPrefix(string s)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return new List<T>();
                }
            }

            return current.GetAllValues();
        }

        public List<KeyValuePair<string, T>> GetPairsWithPrefix(string s)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return new List<KeyValuePair<string, T>>();
                }
            }

            return current.GetAllPairs();
        }

        public bool ContainsPair(string s, T value)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return false;
                }
            }

            return current._endOfWord && current._value.Equals(value);
        }

        public bool Contains(string s, bool prefix = false)
        {
            var current = this;

            foreach (char c in s)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    return false;
                }
            }

            return prefix || current._endOfWord;
        }

        public bool TryGetValue(string key, out T value)
        {
            var current = this;

            foreach (char c in key)
            {
                if (current._dictionary.ContainsKey(c))
                {
                    current = current._dictionary[c];
                }
                else
                {
                    value = default;
                    return false;
                }
            }

            value = current._value;
            return current._endOfWord;
        }
    }
}

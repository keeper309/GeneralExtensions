using System.Collections.Generic;

namespace GameCore.GeneralExtensions
{
    public class DictionaryOfLists<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> _dictionary;

        public IEnumerable<TKey> Keys => _dictionary.Keys;
        public IEnumerable<List<TValue>> Values => _dictionary.Values;

        public List<TValue> this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public DictionaryOfLists(int capacity)
        {
            _dictionary = new Dictionary<TKey, List<TValue>>(capacity);
        }

        public DictionaryOfLists()
        {
            _dictionary = new Dictionary<TKey, List<TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            if (!_dictionary.TryGetValue(key, out List<TValue> list))
            {
                list = new List<TValue>();
                _dictionary.Add(key, list);
            }

            list.Add(value);
        }

        public void Add(TKey key, IEnumerable<TValue> values)
        {
            if (!_dictionary.TryGetValue(key, out List<TValue> list))
            {
                list = new List<TValue>();
                _dictionary.Add(key, list);
            }

            list.AddRange(values);
        }

        public bool TryGetValue(TKey key, out IReadOnlyList<TValue> list)
        {
            bool result = _dictionary.TryGetValue(key, out List<TValue> listRaw);
            list = listRaw;

            return result;
        }

        public bool TryRemove(TKey key, TValue value)
        {
            if (!_dictionary.TryGetValue(key, out List<TValue> list))
            {
                return false;
            }

            return list.Remove(value);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}

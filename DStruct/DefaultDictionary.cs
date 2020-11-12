using System.Collections.Generic;

namespace DStruct
{
    /// <summary>Represents a collection of keys and values with an enhanced indexer.</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
        where TValue : new()
    {
        /// <summary>Gets or sets the value associated with the specified <paramref name="key"/>.</summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns></returns>
        public new TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var value))
                {
                    value = new TValue();
                    Add(key, value);
                }
                
                return value;
            }
            set
            {
                if (!ContainsKey(key))
                {
                    Add(key, value);
                }
                else
                {
                    base[key] = value;
                }
            }
        }
    }
}

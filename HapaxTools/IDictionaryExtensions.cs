using System;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key if it exists, otherwise creates a new instance
        /// of the dictionary's value type, adds it to that key in the dictionary and returns it.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values.</typeparam>
        /// <param name="dict">The current dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>The value associated with the specified key, or a new instance of the dictionary's value type.</returns>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key if it exists, otherwise adds the specified
        /// default value to that key in the dictionary and returns that default value.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values.</typeparam>
        /// <param name="dict">The current dictionary.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultValue">The default value to add to the dictionary and return if the key is not found.</param>
        /// <returns>The value associated with the specified key, or the specified default value.</returns>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue value;

            if (!dict.TryGetValue(key, out value))
            {
                value = defaultValue;
                dict.Add(key, value);
            }

            return value;
        }
    }
}

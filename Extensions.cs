using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralUtils {
    public static class Extensions {
        #region IEnumerable

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable) {
            return new HashSet<T>(enumerable);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer = null) {
            comparer ??= EqualityComparer<T>.Default;

            var index = 0;
            foreach (var item in source) {
                if (comparer.Equals(item, value))
                    return index;
                index++;
            }

            return -1;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var item in enumerable) {
                action(item);
            }
        }

        public static Dictionary<TKey, TValue> ZipToDictionary<TKey, TValue>(this IEnumerable<TKey> keys, IEnumerable<TValue> values) {
            return keys.Zip(values, (k, v) => (k, v)).ToDictionary(pair => pair.k, pair => pair.v);
        }

        #endregion

        #region Dictionary

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue) {
            return dictionary.GetValue(key, () => defaultValue);
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> defaultValueInstantiator) {
            return dictionary.GetValue(key, new Lazy<TValue>(defaultValueInstantiator));
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            Lazy<TValue> defaultValue) {
            if (dictionary.TryGetValue(key, out var result)) {
                return result;
            }

            dictionary[key] = defaultValue.Value;
            return defaultValue.Value;
        }

        #endregion

        #region KeyValuePair

        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key,
            out TValue value) {
            key = pair.Key;
            value = pair.Value;
        }

        #endregion
    }
}

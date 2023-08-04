using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> match) {
            var index = 0;
            foreach (var item in source) {
                if (match(item))
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

        public static bool DictEqual<TKey, TValue>(this IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2) {
            return dictionary1
                .OrderBy(kvp => kvp.Key)
                .SequenceEqual(dictionary2.OrderBy(kvp => kvp.Key));
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> enumerable) {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) {
                throw new ArgumentException("Can't cycle empty enumerable");
            }

            while (true) {
                yield return enumerator.Current;

                if (!enumerator.MoveNext()) {
                    enumerator.Reset();
                    enumerator.MoveNext();
                }
            }
        }
        
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize) {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext()) 
                yield return YieldBatchElements(enumerator, batchSize);
        } 

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize) { 
            yield return source.Current; 
            for (var i = 0; i < batchSize - 1 && source.MoveNext(); i++) 
                yield return source.Current; 
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

        public static Dictionary<TKey, TValue> Copy<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {
            return dictionary.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion

        #region KeyValuePair

        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key,
            out TValue value) {
            key = pair.Key;
            value = pair.Value;
        }

        #endregion

        #region Color

        public static Color WithAlpha(this Color color, float alpha) {
            var result = color;
            result.a = alpha;
            return result;
        }

        #endregion

        #region Vector3

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
            var newVector = vector;

            if (x is { } newX) {
                newVector.x = newX;
            }

            if (y is { } newY) {
                newVector.y = newY;
            }

            if (z is { } newZ) {
                newVector.z = newZ;
            }

            return newVector;
        }

        #endregion

        #region Vector2

        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null) {
            var newVector = vector;

            if (x is { } newX) {
                newVector.x = newX;
            }

            if (y is { } newY) {
                newVector.y = newY;
            }

            return newVector;
        }

        #endregion
    }
}

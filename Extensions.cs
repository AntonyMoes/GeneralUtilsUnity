using System.Collections.Generic;

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

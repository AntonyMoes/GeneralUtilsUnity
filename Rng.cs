using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralUtils {
    public class Rng {
        private readonly System.Random _rnd;
        public int Seed { get; }

        public Rng(int seed) {
            Seed = seed;
            _rnd = new System.Random(seed);
        }

        public static int RandomSeed => UnityEngine.Random.Range(0, int.MaxValue);

        public int NextInt(int min, int max) {
            return _rnd.Next(min, max);
        }

        public float NextFloat(float min, float max) {
            return min + (float) _rnd.NextDouble() * (max - min);
        }

        public T NextChoice<T>(IReadOnlyList<T> collection, out int index) {
            index = NextInt(0, collection.Count);
            return collection[index];
        }

        public T NextChoice<T>(IReadOnlyList<T> collection) {
            return NextChoice(collection, out _);
        }

        public T NextWeightedChoice<T>(IReadOnlyList<T> collection, IReadOnlyList<float> weights, out int index) {
            if (collection.Count != weights.Count) {
                throw new ArgumentException();
            }

            var totalWeight = weights.Sum();
            var value = NextFloat(0, totalWeight);

            float sum = 0;
            index = 0;
            foreach (var item in collection) {
                var weight = weights[index];
                if (value <= sum + weight)
                    return item;

                sum += weight;
                index++;
            }

            throw new ApplicationException();
        }

        public T NextWeightedChoice<T>(IReadOnlyList<(T item, float weight)> collection, out int index) {
            var totalWeight = collection.Sum(t => t.weight);
            var value = NextFloat(0, totalWeight);

            float sum = 0;
            index = 0;
            foreach (var (item, weight) in collection) {
                if (value <= sum + weight)
                    return item;

                sum += weight;
                index++;
            }

            throw new ApplicationException();
        }

        public T NextWeightedChoice<T>(IReadOnlyList<(T item, float weight)> collection) {
            return NextWeightedChoice(collection, out _);
        }

        public List<T> NextShuffle<T>(IEnumerable<T> collection) {
            return collection.OrderBy(_ => NextFloat(0, 1)).ToList();
        }

        public List<T> NextWeightedShuffle<T>(IEnumerable<(T, float)> collection) {
            var pool = new List<(T item, float weight)>(collection);
            var result = new List<T>();
            while (pool.Count > 0) {
                var element = NextWeightedChoice(pool, out var index);
                result.Add(element);
                pool.RemoveAt(index);
            }

            return result;
        }
    }
}
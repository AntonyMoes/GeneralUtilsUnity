using System;
using System.Collections.Generic;

namespace GeneralUtils {
    public class ValueWaiter<T> {
        private readonly Dictionary<Func<T, bool>, Action> _waiters = new Dictionary<Func<T, bool>, Action>();

        private T _value;
        public T Value {
            get => _value;
            set {
                _value = value;

                var toRemove = new List<Func<T, bool>>();
                var activated = new List<Action>();
                foreach (var (predicate, waiter) in _waiters) {
                    if (predicate(Value)) {
                        toRemove.Add(predicate);
                        activated.Add(waiter);
                    }
                }

                foreach (var predicate in toRemove) {
                    _waiters.Remove(predicate);
                }

                foreach (var waiter in activated) {
                    waiter?.Invoke();
                }
            }
        }

        public ValueWaiter(T initialValue = default) {
            _value = initialValue;
        }

        public void WaitForChange(Action onDone) {
            // HACK
            var counter = 0;
            bool SecondTimePredicate(T value) {
                return counter++ >= 1;
            }

            WaitFor(SecondTimePredicate, onDone);
        }

        public void WaitFor(T concreteValue, Action onDone) {
            WaitFor(value => value.Equals(concreteValue), onDone);
        }

        public void WaitFor(Func<T, bool> predicate, Action onDone) {
            if (predicate(Value)) {
                onDone?.Invoke();
            } else {
                _waiters.Add(predicate, onDone);
            }
        }
    }
}

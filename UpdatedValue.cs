using System;
using System.Collections.Generic;

namespace GeneralUtils {
    public class UpdatedValue<T> : IUpdatedValue<T> {
        private readonly Dictionary<Func<T, bool>, Action> _waiters = new Dictionary<Func<T, bool>, Action>();
        private readonly List<Action<T>> _subscribers = new List<Action<T>>();

        private readonly Func<T, T> _setter;

        private T _value;
        public T Value {
            get => _value;
            set {
                _value = _setter == null ? value : _setter(value);

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

                foreach (var subscriber in _subscribers) {
                    subscriber?.Invoke(_value);
                }
            }
        }

        public UpdatedValue(T initialValue = default, Func<T, T> setter = null) {
            _value = initialValue;
            _setter = setter;
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

        public void Subscribe(Action<T> onChange, bool triggerInitialUpdate = false) {
            _subscribers.Add(onChange);

            if (triggerInitialUpdate) {
                onChange?.Invoke(Value);
            }
        }

        public void Unsubscribe(Action<T> onChange) {
            _subscribers.Remove(onChange);
        }

        public void Clear() {
            _waiters.Clear();
            _subscribers.Clear();
        }
    }

    public interface IUpdatedValue<T> {
        public T Value { get; }

        public void WaitForChange(Action onDone);
        public void WaitFor(T concreteValue, Action onDone);
        public void WaitFor(Func<T, bool> predicate, Action onDone);

        public void Subscribe(Action<T> onChange, bool triggerInitialUpdate = false);
        public void Unsubscribe(Action<T> onChange);

        public void Clear();
    }
}

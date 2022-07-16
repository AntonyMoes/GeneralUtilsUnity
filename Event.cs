using System;

namespace GeneralUtils {
    public class Event {
        private Action _event;

        public Event(out Action invoker) {
            invoker = () => _event?.Invoke();
        }

        public void Subscribe(Action subscriber) {
            _event += subscriber;
        }

        public void Unsubscribe(Action subscriber) {
            _event -= subscriber;
        }

        public void ClearSubscribers() {
            _event = null;
        }
    }

    public class Event<T> {
        private Action<T> _event;

        public Event(out Action<T> invoker) {
            invoker = value => _event?.Invoke(value);
        }

        public void Subscribe(Action<T> subscriber) {
            _event += subscriber;
        }

        public void Unsubscribe(Action<T> subscriber) {
            _event -= subscriber;
        }

        public void ClearSubscribers() {
            _event = null;
        }
    }

    public class Event<T1, T2> {
        private Action<T1, T2> _event;

        public Event(out Action<T1, T2> invoker) {
            invoker = (v1, v2) => _event?.Invoke(v1, v2);
        }

        public void Subscribe(Action<T1, T2> subscriber) {
            _event += subscriber;
        }

        public void Unsubscribe(Action<T1, T2> subscriber) {
            _event -= subscriber;
        }

        public void ClearSubscribers() {
            _event = null;
        }
    }

    public class Event<T1, T2, T3> {
        private Action<T1, T2, T3> _event;

        public Event(out Action<T1, T2, T3> invoker) {
            invoker = (v1, v2, v3) => _event?.Invoke(v1, v2, v3);
        }

        public void Subscribe(Action<T1, T2, T3> subscriber) {
            _event += subscriber;
        }

        public void Unsubscribe(Action<T1, T2, T3> subscriber) {
            _event -= subscriber;
        }

        public void ClearSubscribers() {
            _event = null;
        }
    }
}

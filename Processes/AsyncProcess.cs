using System;

namespace GeneralUtils.Processes {
    public class AsyncProcess : Process {
        private readonly Action<Action> _asyncAction;

        public AsyncProcess(Action<Action> asyncAction) {
            _asyncAction = asyncAction;
        }

        protected override void PerformRun() {
            _asyncAction(() => {
                if (State != EState.Aborted) {
                    Finish();
                }
            });
        }

        protected override void PerformAbort() { }

        public static AsyncProcess From<T>(Action<T, Action> action, T arg) {
            return new AsyncProcess(onDone => action(arg, onDone));
        }

        public static AsyncProcess From<T1, T2>(Action<T1, T2, Action> action, T1 arg1, T2 arg2) {
            return new AsyncProcess(onDone => action(arg1, arg2, onDone));
        }

        public static AsyncProcess From<T1, T2, T3>(Action<T1, T2, T3, Action> action, T1 arg1, T2 arg2, T3 arg3) {
            return new AsyncProcess(onDone => action(arg1, arg2, arg3, onDone));
        }
    }
}

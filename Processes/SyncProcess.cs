using System;

namespace GeneralUtils.Processes {
    public class SyncProcess : Process {
        private readonly Action _action;

        public SyncProcess(Action action) {
            _action = action;
        }

        protected override void PerformRun() {
            _action();
            if (State != EState.Aborted) {
                Finish();
            }
        }

        protected override void PerformAbort() { }

        public static SyncProcess From<T>(Action<T> action, T arg) {
            return new SyncProcess(() => action(arg));
        }

        public static SyncProcess From<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2) {
            return new SyncProcess(() => action(arg1, arg2));
        }

        public static SyncProcess From<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3) {
            return new SyncProcess(() => action(arg1, arg2, arg3));
        }
    }
}

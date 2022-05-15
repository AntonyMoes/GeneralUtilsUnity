using System;

namespace GeneralUtils.Processes {
    public abstract class Process {
        public enum EState {
            Standby,
            Running,
            Aborted,
            Finished
        }

        private Action _onDone;
        public EState State { get; private set; }

        public void Run(Action onDone) {
            CheckAndSwitchState(EState.Standby, EState.Running);
            _onDone = onDone;
            PerformRun();
        }

        protected abstract void PerformRun();

        public void Abort() {
            CheckAndSwitchState(EState.Running, EState.Aborted);
            PerformAbort();
        }

        protected abstract void PerformAbort();

        protected void Finish() {
            CheckAndSwitchState(EState.Running, EState.Finished);
            _onDone?.Invoke();
        }

        private void CheckAndSwitchState(EState expected, EState newState) {
            CheckState(expected);
            State = newState;
        }

        protected void CheckState(EState expected) {
            if (State != expected) {
                throw new ApplicationException($"Expected {expected} state but got {State}");
            }
        }
    }
}

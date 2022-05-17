using System;
using System.Linq;

namespace GeneralUtils.Processes {
    public abstract class Process {
        public enum EState {
            Standby,
            Running,
            Aborted,
            Finished
        }

        private Action _onAbort;

        private Action _onDone;
        public EState State { get; private set; } = EState.Standby;

        public void Run(Action onDone = null, Action onAbort = null) {
            CheckAndSwitchState(EState.Running, EState.Standby);
            _onDone = onDone;
            _onAbort = onAbort;
            PerformRun();
        }

        protected abstract void PerformRun();

        public void TryAbort() {
            switch (State) {
                case EState.Standby:
                case EState.Running:
                    CheckAndSwitchState(EState.Aborted, EState.Running, EState.Standby);
                    PerformAbort();
                    _onAbort?.Invoke();
                    break;
                case EState.Aborted:
                case EState.Finished:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void PerformAbort();

        protected void Finish() {
            CheckAndSwitchState(EState.Finished, EState.Running);
            _onDone?.Invoke();
        }

        private void CheckAndSwitchState(EState newState, params EState[] expected) {
            CheckState(expected);
            State = newState;
        }

        protected void CheckState(params EState[] expected) {
            if (!expected.Contains(State)) {
                throw new ApplicationException($"Expected {string.Join(" or ", expected)} state but got {State}");
            }
        }
    }
}

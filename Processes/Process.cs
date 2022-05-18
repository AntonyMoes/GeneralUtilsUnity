using System;

namespace GeneralUtils.Processes {
    public abstract class Process {
        public enum EState {
            Standby,
            Running,
            Aborted,
            Finished
        }

        private readonly StateSwitcher<EState> _stateSwitcher = new StateSwitcher<EState>(EState.Standby);
        private Action _onAbort;
        private Action _onDone;

        public EState State => _stateSwitcher.State;

        public void Run(Action onDone = null, Action onAbort = null) {
            _stateSwitcher.CheckAndSwitchState(EState.Running, EState.Standby);
            _onDone = onDone;
            _onAbort = onAbort;
            PerformRun();
        }

        protected abstract void PerformRun();

        public void TryAbort() {
            switch (State) {
                case EState.Standby:
                case EState.Running:
                    _stateSwitcher.CheckAndSwitchState(EState.Aborted, EState.Running, EState.Standby);
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
            _stateSwitcher.CheckAndSwitchState(EState.Finished, EState.Running);
            _onDone?.Invoke();
        }

        protected void CheckState(params EState[] expected) {
            _stateSwitcher.CheckState(expected);
        }
    }
}

using System;

namespace GeneralUtils.Command {
    public abstract class Command {
        public enum EState {
            NotDone,
            Done
        }

        public EState State { get; private set; } = EState.NotDone;

        public void Do() {
            CheckAndSwitchState(EState.NotDone, EState.Done);
            PerformDo();
        }

        protected abstract void PerformDo();

        protected void CheckAndSwitchState(EState expected, EState newState) {
            CheckState(expected);
            State = newState;
        }

        private void CheckState(EState expected) {
            if (State != expected) {
                throw new ApplicationException($"Expected {expected} state but got {State}");
            }
        }
    }
}

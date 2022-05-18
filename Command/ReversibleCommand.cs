using System;

namespace GeneralUtils.Command {
    public abstract class ReversibleCommand : Command {
        private Action _performUndo;

        protected sealed override void PerformDo() {
            _performUndo = PerformReversibleDo();
        }

        protected abstract Action PerformReversibleDo();

        public void Undo() {
            CheckAndSwitchState(EState.NotDone, EState.Done);
            _performUndo();
        }
    }
}

namespace GeneralUtils.Command {
    public abstract class Command {
        public enum EState {
            NotDone,
            Done
        }

        private readonly StateSwitcher<EState> _stateSwitcher = new StateSwitcher<EState>(EState.NotDone);
        public EState State => _stateSwitcher.State;

        public void Do() {
            _stateSwitcher.CheckAndSwitchState(EState.Done, EState.NotDone);
            PerformDo();
        }

        protected abstract void PerformDo();

        protected void CheckAndSwitchState(EState newState, params EState[] expected) {
            _stateSwitcher.CheckAndSwitchState(newState, expected);
        }
    }
}

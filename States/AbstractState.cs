using System;

namespace GeneralUtils.States {
    public abstract class AbstractState<TStateEnum> : IState<TStateEnum>
        where TStateEnum : struct, Enum {
        private Action<TStateEnum?, StateInfo> _stateSwitcher;

        public void SetStateSwitcher(Action<TStateEnum?, StateInfo> stateSwitcher) {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void OnStateEnter(StateInfo stateInfo = null);

        public virtual void OnStateExit() { }

        public virtual void OnStatePersist(float deltaTime) { }

        protected void SwitchState(TStateEnum stateName, StateInfo stateInfo = null) {
            _stateSwitcher(stateName, stateInfo);
        }
    }
}

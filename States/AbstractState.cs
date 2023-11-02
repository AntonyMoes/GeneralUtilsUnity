using System;

namespace GeneralUtils.States {
    public abstract class AbstractState<TStateEnum> : IState<TStateEnum>
        where TStateEnum : struct, Enum {
        private Action<TStateEnum?, IStateInfo> _stateSwitcher;

        public void SetStateSwitcher(Action<TStateEnum?, IStateInfo> stateSwitcher) {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void OnStateEnter(IStateInfo stateInfo = null);

        public virtual void OnStateExit() { }

        public virtual void OnStatePersist(float deltaTime) { }

        protected void SwitchState(TStateEnum stateName, IStateInfo stateInfo = null) {
            _stateSwitcher(stateName, stateInfo);
        }
    }
}

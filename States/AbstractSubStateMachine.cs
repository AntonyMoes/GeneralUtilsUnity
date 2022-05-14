using System;

namespace GeneralUtils.States {
    public abstract class AbstractSubStateMachine<TBaseStateEnum, TStateEnum> : StateMachine<TStateEnum>, IState<TBaseStateEnum>
        where TBaseStateEnum : struct, Enum
        where TStateEnum : struct, Enum {
        private Action<TBaseStateEnum?, StateInfo> _stateSwitcher;

        public void SetStateSwitcher(Action<TBaseStateEnum?, StateInfo> stateSwitcher) {
            _stateSwitcher = stateSwitcher;
        }

        public void OnStateEnter(StateInfo stateInfo = null) {
            Start();
            OnStateEnterLogic(stateInfo);
        }

        protected abstract void OnStateEnterLogic(StateInfo stateInfo = null);

        public void OnStateExit() {
            Stop();
            PerformStateExit();
        }

        public void OnStatePersist(float deltaTime) {
            Update(deltaTime);
            PerformStatePersist(deltaTime);
        }

        protected virtual void PerformStateExit() { }

        protected virtual void PerformStatePersist(float deltaTime) { }

        protected void SwitchState(TBaseStateEnum stateName, StateInfo stateInfo = null) {
            _stateSwitcher(stateName, stateInfo);
        }
    }
}

using System;

namespace GeneralUtils.States {
    public abstract class AbstractSubStateMachine<TBaseStateEnum, TStateEnum> : StateMachine<TStateEnum>, IState<TBaseStateEnum>
        where TBaseStateEnum : struct, Enum
        where TStateEnum : struct, Enum {
        private Action<TBaseStateEnum?, IStateInfo> _stateSwitcher;

        public void SetStateSwitcher(Action<TBaseStateEnum?, IStateInfo> stateSwitcher) {
            _stateSwitcher = stateSwitcher;
        }

        public void OnStateEnter(IStateInfo stateInfo = null) {
            OnStateEnterLogic(stateInfo);
            Start();
        }

        protected abstract void OnStateEnterLogic(IStateInfo stateInfo = null);

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

        protected void SwitchState(TBaseStateEnum stateName, IStateInfo stateInfo = null) {
            _stateSwitcher(stateName, stateInfo);
        }
    }
}

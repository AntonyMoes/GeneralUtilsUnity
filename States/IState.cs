using System;

namespace GeneralUtils.States {
    public interface IState<TStateEnum> where TStateEnum : struct, Enum {
        public void SetStateSwitcher(Action<TStateEnum?, StateInfo> stateSwitcher);

        public void OnStateEnter(StateInfo stateInfo);
        public void OnStateExit();
        public void OnStatePersist(float deltaTime);
    }
}

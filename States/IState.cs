using System;

namespace GeneralUtils.States {
    public interface IState<TStateEnum> where TStateEnum : struct, Enum {
        public void SetStateSwitcher(Action<TStateEnum?, IStateInfo> stateSwitcher);

        public void OnStateEnter(IStateInfo stateInfo);
        public void OnStateExit();
        public void OnStatePersist(float deltaTime);
    }
}

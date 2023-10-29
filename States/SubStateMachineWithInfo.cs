using System;

namespace GeneralUtils.States {
    public class SubStateMachineWithInfo<TBaseStateEnum, TStateEnum, TStateInfo> : AbstractSubStateMachine<TBaseStateEnum, TStateEnum>
        where TBaseStateEnum : struct, Enum
        where TStateEnum : struct, Enum
        where TStateInfo : StateInfo {
        protected sealed override void OnStateEnterLogic(StateInfo stateInfo = null) {
            PerformStateEnter(stateInfo as TStateInfo);
        }

        protected virtual void PerformStateEnter(StateInfo stateInfo = null) { }
    }
}

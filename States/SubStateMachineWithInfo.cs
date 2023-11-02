using System;

namespace GeneralUtils.States {
    public class SubStateMachineWithInfo<TBaseStateEnum, TStateEnum, TStateInfo> : AbstractSubStateMachine<TBaseStateEnum, TStateEnum>
        where TBaseStateEnum : struct, Enum
        where TStateEnum : struct, Enum
        where TStateInfo : class, IStateInfo {
        protected sealed override void OnStateEnterLogic(IStateInfo stateInfo = null) {
            PerformStateEnter(stateInfo as TStateInfo);
        }

        protected virtual void PerformStateEnter(IStateInfo stateInfo = null) { }
    }
}

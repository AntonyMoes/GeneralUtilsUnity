using System;

namespace GeneralUtils.States {
    public abstract class SubStateMachine<TBaseStateEnum, TStateEnum> : AbstractSubStateMachine<TBaseStateEnum, TStateEnum>
        where TBaseStateEnum : struct, Enum
        where TStateEnum : struct, Enum {
        protected sealed override void OnStateEnterLogic(IStateInfo stateInfo = null) {
            PerformStateEnter();
        }

        protected virtual void PerformStateEnter() { }
    }
}

using System;

namespace GeneralUtils.States {
    public abstract class State<TStateEnum> : AbstractState<TStateEnum>
        where TStateEnum : struct, Enum {
        public sealed override void OnStateEnter(StateInfo stateInfo = null) {
            PerformStateEnter();
        }

        protected virtual void PerformStateEnter() { }
    }
}

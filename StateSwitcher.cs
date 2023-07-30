using System;
using System.Linq;

namespace GeneralUtils {
    public class StateSwitcher<TEState> where TEState : Enum {
        private readonly UpdatedValue<TEState> _state;
        public IUpdatedValue<TEState> State => _state;

        public StateSwitcher(TEState initialState) {
            _state = new UpdatedValue<TEState>(initialState);
        }

        public void CheckAndSwitchState(TEState newState, params TEState[] expected) {
            CheckState(expected);
            _state.Value = newState;
        }

        public void CheckState(params TEState[] expected) {
            if (expected.Length > 0 && !expected.Contains(_state.Value)) {
                throw new ApplicationException($"Expected {string.Join(" or ", expected)} state but got {State}");
            }
        }
    }
}

using System;
using System.Linq;

namespace GeneralUtils {
    public class StateSwitcher<TEState> where TEState : Enum {
        public StateSwitcher(TEState initialState) {
            State = initialState;
        }

        public TEState State { get; private set; }

        public void CheckAndSwitchState(TEState newState, params TEState[] expected) {
            CheckState(expected);
            State = newState;
        }

        public void CheckState(params TEState[] expected) {
            if (expected.Length > 0 && !expected.Contains(State)) {
                throw new ApplicationException($"Expected {string.Join(" or ", expected)} state but got {State}");
            }
        }
    }
}

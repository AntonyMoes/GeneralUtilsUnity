using System;
using System.Collections.Generic;

namespace GeneralUtils.States {
    public class StateMachine<TStateEnum> where TStateEnum : struct, Enum {
        private readonly Dictionary<TStateEnum, IState<TStateEnum>> _states =
            new Dictionary<TStateEnum, IState<TStateEnum>>();

        private TStateEnum? _currentState;
        private TStateEnum? _defaultState;

        public void SetDefaultState(TStateEnum stateName) {
            _defaultState = stateName;
        }

        public void AddState(TStateEnum stateName, IState<TStateEnum> state) {
            if (_states.ContainsKey(stateName)) {
                throw new ArgumentException($"StateMachine already has such state: {stateName}", nameof(stateName));
            }

            _states.Add(stateName, state);
            state.SetStateSwitcher(SwitchToState);
        }

        public void Start() {
            if (!(_defaultState is { } defaultState)) {
                throw new ArgumentNullException(nameof(_defaultState), "Default state not set");
            }

            if (!_states.ContainsKey(defaultState)) {
                throw new ArgumentException($"StateMachine does not contain the state set as default: {defaultState}",
                    nameof(_defaultState));
            }

            SwitchToState(defaultState, null);
        }

        public void Stop() {
            SwitchToState(null, null);
        }

        public void Update(float deltaTime) {
            if (!(_currentState is { } currentState)) {
                return;
            }

            if (!_states.ContainsKey(currentState)) {
                throw new ArgumentException($"StateMachine does not contain the state set as current: {currentState}",
                    nameof(_currentState));
            }

            _states[currentState].OnStatePersist(deltaTime);
        }

        private void SwitchToState(TStateEnum? stateName, StateInfo stateInfo) {
            if (stateName is { } state && !_states.ContainsKey(state)) {
                throw new ArgumentException($"StateMachine does not contain the target state: {stateName}",
                    nameof(stateName));
            }

            if (_currentState is { } currentState) {
                _states[currentState].OnStateExit();
            }

            _currentState = stateName;
            if (_currentState is { } newCurrentState) {
                _states[newCurrentState].OnStateEnter(stateInfo);
            }
        }
    }
}

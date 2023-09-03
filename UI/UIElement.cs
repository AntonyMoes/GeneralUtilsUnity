using System;
using UnityEngine;

namespace GeneralUtils.UI {
    public class UIElement : MonoBehaviour {
        private readonly UpdatedValue<EState> _state = new UpdatedValue<EState>(EState.Hided);
        public IUpdatedValue<EState> State => _state;

        protected virtual bool ClearOnHide => true;

        private void Awake() {
            Init();
        }

        protected virtual void Init() { }

        public void Show(Action onDone = null) {
            if (_state.Value == EState.Shown) {
                onDone?.Invoke();
                return;
            }

            if (_state.Value == EState.Showing) {
                _state.WaitFor(EState.Shown, onDone);
                return;
            }

            // TODO what to do if Hiding?

            _state.Value = EState.Showing;
            gameObject.SetActive(true);

            PerformShow(() => {
                _state.Value = EState.Shown;
                onDone?.Invoke();
            });
        }

        protected virtual void PerformShow(Action onDone = null) {
            onDone?.Invoke();
        }

        public void Hide(Action onDone = null) {
            if (_state.Value == EState.Hided) {
                onDone?.Invoke();
                return;
            }

            if (_state.Value == EState.Hiding) {
                _state.WaitFor(EState.Hided, onDone);
                return;
            }

            // TODO what to do if Showing?

            _state.Value = EState.Hiding;

            PerformHide(() => {
                gameObject.SetActive(false);
                _state.Value = EState.Hided;

                if (ClearOnHide) {
                    Clear();
                }

                onDone?.Invoke();
            });
        }

        protected virtual void PerformHide(Action onDone = null) {
            onDone?.Invoke();
        }

        public virtual void Clear() { }

        public enum EState {
            Showing,
            Shown,
            Hiding,
            Hided
        }
    }
}
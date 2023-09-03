using System;
using UnityEngine;

namespace GeneralUtils.UI {
    public class UIElement : MonoBehaviour {
        private readonly UpdatedValue<EState> _state = new UpdatedValue<EState>(EState.Hided);
        public IUpdatedValue<EState> State => _state;

        private CanvasGroup _group;

        protected virtual bool ClearOnHide => true;

        protected virtual bool ChangeInteractivity => true;

        private void Awake() {
            Init();
            _group = TryGetComponent<CanvasGroup>(out var group) ? group : gameObject.AddComponent<CanvasGroup>();
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

            if (ChangeInteractivity) {
                _group.interactable = false;
            } else {
                _group.blocksRaycasts = false;
            }

            PerformShow(() => {
                if (ChangeInteractivity) {
                    _group.interactable = true;
                } else {
                    _group.blocksRaycasts = true;
                }

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

            if (ChangeInteractivity) {
                _group.interactable = false;
            } else {
                _group.blocksRaycasts = false;
            }

            PerformHide(() => {
                if (ChangeInteractivity) {
                    _group.interactable = true;
                } else {
                    _group.blocksRaycasts = true;
                }

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
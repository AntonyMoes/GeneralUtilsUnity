using System;
using UnityEngine;

namespace GeneralUtils.UI {
    public class UIElement : MonoBehaviour {
        private readonly UpdatedValue<EState> _state = new UpdatedValue<EState>(EState.Hided);
        public IUpdatedValue<EState> State => _state;

        private CanvasGroup _group;

        protected virtual bool ClearOnHide => true;

        protected virtual bool ChangeInteractivity => true;

        private bool _locked;
        public bool Locked {
            get => _locked;
            set {
                _locked = value;
                SetGroupInteractable(value, Interactable);
            }
        }

        private bool _interactable;
        private bool Interactable {
            get => _interactable;
            set {
                _interactable = value;
                SetGroupInteractable(Locked, Interactable);
            }
        }

        private void Awake() {
            Init();
            _group = TryGetComponent<CanvasGroup>(out var group) ? group : gameObject.AddComponent<CanvasGroup>();
        }

        private void SetGroupInteractable(bool locked, bool interactable) {
            var groupInteractable = !locked && interactable;
            if (ChangeInteractivity) {
                _group.interactable = groupInteractable;
            } else {
                _group.blocksRaycasts = groupInteractable;
            }
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

            Interactable = false;

            PerformShow(() => {
                Interactable = true;

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

            Interactable = false;

            PerformHide(() => {
                Interactable = true;

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
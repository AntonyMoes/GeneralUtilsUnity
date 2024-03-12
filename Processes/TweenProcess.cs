using DG.Tweening;

namespace GeneralUtils.Processes {
    public class TweenProcess : Process {
        private readonly Tween _tween;

        public TweenProcess(Tween tween) {
            _tween = tween;
            _tween.Pause();
        }

        protected override void PerformRun() {
            _tween.OnComplete(Finish).Play();
        }

        protected override void PerformAbort() {
            _tween.Kill();
        }
    }
}
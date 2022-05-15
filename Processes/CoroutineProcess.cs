using System.Collections;
using UnityEngine;

namespace GeneralUtils.Processes {
    public class CoroutineProcess : Process {
        private readonly MonoBehaviour _coroutineRunner;
        private readonly IEnumerator _routine;
        private Coroutine _coroutine;

        public CoroutineProcess(MonoBehaviour coroutineRunner, IEnumerator routine) {
            _coroutineRunner = coroutineRunner;
            _routine = routine;
        }

        protected override void PerformRun() {
            _coroutine = _coroutineRunner.StartCoroutine(InternalRoutine(_routine));

            IEnumerator InternalRoutine(IEnumerator routine) {
                yield return routine;

                if (State != EState.Aborted) {
                    Finish();
                }
            }
        }

        protected override void PerformAbort() {
            _coroutineRunner.StopCoroutine(_coroutine);
        }
    }
}

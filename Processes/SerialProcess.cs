using System.Collections.Generic;

namespace GeneralUtils.Processes {
    public class SerialProcess : MultiProcess {
        private readonly List<Process> _items = new List<Process>();
        private int _currentIndex;

        protected override void PerformRun() {
            RunNextItem();
        }

        private void RunNextItem() {
            if (_currentIndex >= _items.Count) {
                Finish();
                return;
            }

            _items[_currentIndex].Run(() => {
                if (State == EState.Aborted) {
                    return;
                }

                _currentIndex++;
                RunNextItem();
            });
        }

        protected override void PerformAbort() {
            _items[_currentIndex].Abort();
        }

        protected override void PerformAdd(Process subProcess) {
            _items.Add(subProcess);
        }
    }
}

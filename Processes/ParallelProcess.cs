using System.Collections.Generic;

namespace GeneralUtils.Processes {
    public class ParallelProcess : MultiProcess {
        private readonly List<Process> _items = new List<Process>();
        private int _completedCount;

        protected override void PerformRun() {
            foreach (var item in _items) {
                if (State.Value == EState.Aborted) {
                    return;
                }

                item.Run(() => {
                    if (State.Value == EState.Aborted) {
                        return;
                    }

                    _completedCount++;
                    if (_completedCount == _items.Count) {
                        Finish();
                    }
                });
            }
        }

        protected override void PerformAbort() {
            foreach (var item in _items) {
                item.TryAbort();
            }
        }

        protected override void PerformAdd(Process subProcess) {
            _items.Add(subProcess);
        }
    }
}

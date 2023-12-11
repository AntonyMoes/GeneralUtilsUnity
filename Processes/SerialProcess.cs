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
                if (State.Value == EState.Aborted) {
                    return;
                }

                _currentIndex++;
                RunNextItem();
            });
        }

        protected override void PerformAbort() {
            foreach (var item in _items) {
                item.TryAbort();
            }
        }

        protected override void PerformAdd(Process subProcess) {
            _items.Add(subProcess);
        }

        public static SerialProcess From(params Process[] processes) {
            var serialProcess = new SerialProcess();
            foreach (var process in processes) {
                serialProcess.Add(process);
            }

            return serialProcess;
        }
    }
}

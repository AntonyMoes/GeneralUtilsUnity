using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralUtils.Processes {
    public class ConditionalProcess : Process {
        private readonly List<Item> _items = new List<Item>();
        private bool _aborted;
        
        protected override void PerformRun() {
            OnItemComplete();
        }

        private void OnItemComplete() {
            if (_aborted) {
                return;
            }

            if (_items.All(item => item.IsComplete)) {
                Finish();
                return;
            }

            foreach (var item in _items) {
                if (item.IsReady) {
                    item.Run(OnItemComplete);
                }
            }
        }

        protected override void PerformAbort() {
            _aborted = true;
        }

        public IItem Add(Process subProcess) {
            CheckState(EState.Standby);

            var item = new Item(subProcess);
            _items.Add(item);
            return item;
        }

        public interface IItem {
            void AddDependencies(params IItem[] items);
            public bool IsComplete { get; }
        }

        private class Item : IItem {
            private readonly Process _process;
            private readonly HashSet<IItem> _dependencies = new HashSet<IItem>();
            private bool _started;

            public bool IsReady => !_started && _dependencies.All(item => item.IsComplete);

            public bool IsComplete => _process.State == EState.Finished;

            public Item(Process process) {
                _process = process;
            }

            public void Run(Action onDone) {
                _started = true;
                _process.Run(onDone);
            }

            public void AddDependencies(params IItem[] items) {
                foreach (var item in items) {
                    _dependencies.Add(item);
                }
            }
        }
    }
}
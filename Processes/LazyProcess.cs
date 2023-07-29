using System;

namespace GeneralUtils.Processes {
    public class LazyProcess : Process {
        private readonly Lazy<Process> _lazyProcess;

        public LazyProcess(Lazy<Process> lazyProcess) {
            _lazyProcess = lazyProcess;
        }

        public LazyProcess(Func<Process> lazyProcess) {
            _lazyProcess = new Lazy<Process>(lazyProcess);
        }

        protected override void PerformRun() {
            _lazyProcess.Value.Run(Finish);
        }

        protected override void PerformAbort() {
            if (_lazyProcess.IsValueCreated) {
                _lazyProcess.Value.TryAbort();
            }
        }
    }
}

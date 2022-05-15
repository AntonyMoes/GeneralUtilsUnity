namespace GeneralUtils.Processes {
    public abstract class MultiProcess : Process {
        public void Add(Process subProcess) {
            CheckState(EState.Standby);
            PerformAdd(subProcess);
        }

        protected abstract void PerformAdd(Process subProcess);
    }
}

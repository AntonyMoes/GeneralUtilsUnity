namespace GeneralUtils.Processes {
    public class DummyProcess : Process {
        protected override void PerformRun() {
            Finish();
        }

        protected override void PerformAbort() { }
    }
}

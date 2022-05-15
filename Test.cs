using System;
using System.Threading;
using System.Threading.Tasks;
using GeneralUtils.Processes;

namespace GeneralUtils {
    internal static class Test {
        public static void RunTest() {
            Console.WriteLine("Test");

            Console.WriteLine("Serial:");
            var serialProcess = new SerialProcess();
            PopulateMultiProcess(serialProcess);
            serialProcess.Run(() => {
                Console.WriteLine("Parallel:");
                var parallelProcess = new ParallelProcess();
                PopulateMultiProcess(parallelProcess);
                parallelProcess.Run(() => { Console.WriteLine("Finished"); });
            });
        }

        private static void PopulateMultiProcess(MultiProcess process) {
            process.Add(CreateAsync(222));
            process.Add(CreateAsync(333));
            process.Add(CreateAsync(111));
            process.Add(CreateAsync(555));
            process.Add(CreateAsync(444));

            static Process CreateAsync(int delay) {
                return new AsyncProcess(onDone => DelayedCall(delay, () => {
                    Console.WriteLine($"Delay: {delay}");
                    onDone();
                }));
            }
        }

        private static void DelayedCall(int delayInMS, Action action) {
            new Thread(() => {
                Thread.Sleep(delayInMS);
                action();
            }).Start();
        }

        public static void RunTestAsync() {
            RunTestAsyncInternal().GetAwaiter().GetResult();
        }

        private static async Task RunTestAsyncInternal() {
            var delay = await Sleep();
            Console.WriteLine($"Delay: {delay}");
        }

        private static async Task<int> Sleep() {
            const int delay = 1000;

            Console.WriteLine("TestAsync");
            await Task.Delay(delay);
            Console.WriteLine("TestAsync after delay");

            return delay;
        }
    }
}

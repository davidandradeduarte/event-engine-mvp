using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MT_Common;

namespace MT_Processor
{
    public class CreatedFooConsumer : IConsumer<CreatedFoo>
    {
        private const int SleepingMs = 5000;
        private int count;
        private bool simulateError;
        private bool sleepMode;

        public async Task Consume(ConsumeContext<CreatedFoo> context)
        {
            count++;
            await Console.Out.WriteLineAsync($"Processing: {context.Message}");

            ErrorMode();
            SleepMode();
        }

        public void ToggleErrorSimulation()
        {
            if (simulateError)
            {
                simulateError = false;
                Console.WriteLine("");
                Console.WriteLine("Error simulation disabled");
                return;
            }
            simulateError = true;
            Console.WriteLine("");
            Console.WriteLine("Error simulation enabled. Will throw on next message!");
        }

        public void ToggleSleepMode()
        {
            if (sleepMode)
            {
                sleepMode = false;
                Console.WriteLine("");
                Console.WriteLine("Sleep Mode disabled");
                return;
            }
            sleepMode = true;
            Console.WriteLine("");
            Console.WriteLine("Sleep Mode enabled");
        }

        private void ErrorMode()
        {
            if (simulateError)
            {
                simulateError = false;
                Console.WriteLine("throwing...");
                throw new InvalidOperationException($"something when wrong on message {count}");
            }
        }

        private void SleepMode()
        {
            if (sleepMode)
            {
                Console.WriteLine("");
                Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId}: Sleep 5s...");
                Thread.Sleep(SleepingMs);
                Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId}: Done!");
            }
        }
    }
}

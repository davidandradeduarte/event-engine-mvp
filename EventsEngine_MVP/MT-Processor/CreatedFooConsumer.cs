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
            Console.WriteLine("");
            if (simulateError)
            {
                simulateError = false;
                Console.WriteLine("Error simulation disabled");
                return;
            }
            simulateError = true;
            Console.WriteLine("Error simulation enabled. Will throw on next message!");
        }

        public void ToggleSleepMode()
        {
            Console.WriteLine("");
            if (sleepMode)
            {
                sleepMode = false;
                Console.WriteLine("Sleep Mode disabled");
                return;
            }
            sleepMode = true;
            Console.WriteLine("Sleep Mode enabled");
        }

        private void ErrorMode()
        {
            if (!simulateError)
            {
                return;
            }

            simulateError = false;

            if (DateTime.UtcNow.Second % 2 == 0)
            {
                Console.WriteLine("      Throwing... should retry");
                Console.WriteLine("");
                throw new InvalidOperationException($"error on message {count} -- should retry");
            }
            Console.WriteLine("      Throwing... dont retry");
            Console.WriteLine("");
            throw new ArgumentException($"error on message {count} -- dont retry");
        }

        private void SleepMode()
        {
            if (!sleepMode)
            {
                return;
            }

            Console.WriteLine("");
            Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId}: Sleep 5s...");
            Thread.Sleep(SleepingMs);
            Console.WriteLine($"thread {Thread.CurrentThread.ManagedThreadId}: Done!");
        }
    }
}

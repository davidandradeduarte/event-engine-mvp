using System;
using System.Messaging;
using System.Threading;
using MT_Common;

namespace EventProcessor
{
    public class Processor
    {
        private const int SleepingMs = 5000;
        private int count;
        private bool simulateError;
        private bool sleepMode;

        public void Process()
        {
            using (var messageQueue = new MessageQueue(Settings.Msmq.Path, QueueAccessMode.Receive))
            {
                messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(Payload) });

                Console.WriteLine("");
                Console.WriteLine("Waiting for messages...");
                var message = messageQueue.Receive();

                ErrorMode();
                var data = message?.Body.ToString() ?? "(null)";
                Console.WriteLine($"Received: '{data}'");
            }

            count++;
            Console.WriteLine($"Message {count} processed.");
            SleepMode();
        }

        private void ErrorMode()
        {
            if (simulateError)
            {
                simulateError = false;
                throw new InvalidOperationException($"something when wrong on message {count}");
            }
        }

        private void SleepMode()
        {
            if (sleepMode)
            {
                Console.WriteLine("");
                Console.WriteLine("Sleep 5s...");
                Thread.Sleep(SleepingMs);
                Console.WriteLine("Back");
            }
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
    }
}

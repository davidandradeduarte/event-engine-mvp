using System;
using System.Messaging;
using System.Threading.Tasks;
using MT_Common;

namespace EventProcessor
{
    public class Program
    {
        private static Processor _consumer;

        static void Main(string[] args)
        {
            Console.WriteLine("Consumer here!");

            if (!MessageQueue.Exists(Settings.Msmq.Path))
            {
                Console.WriteLine($"No queue found on path: '{Settings.Msmq.Path}'");
                Console.WriteLine("Aborting execution.");
                return;
            }

            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("Press 'e' anytime to simulate an error while processing the next message");
            Console.WriteLine("Press 's' anytime to toogle sleep mode");
            Console.WriteLine("");
            var taskKeys = new Task(ReadKeys);

            taskKeys.Start();

            _consumer = new Processor();

            while (!taskKeys.IsCompleted)
            {
                try
                {
                    _consumer.Process();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: '{e.Message}'");
                }
            }

            Task.WaitAll(taskKeys);
        }

        // https://stackoverflow.com/questions/5891538/listen-for-key-press-in-net-console-app#5891612
        private static void ReadKeys()
        {
            while (!Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;
                if (key.Key == ConsoleKey.E) _consumer.ToggleErrorSimulation();
                if (key.Key == ConsoleKey.S) _consumer.ToggleSleepMode();
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace MT_Processor
{
    public class Program
    {
        private static Processor _processor;
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer here!");
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("Press 'e' anytime to simulate an error while processing the next message");
            Console.WriteLine("Press 's' anytime to toogle sleep mode");
            Console.WriteLine("");

            var taskKeys = new Task(ReadKeys);

            taskKeys.Start();

            _processor = new Processor();

            Task.WaitAll(taskKeys);

            _processor?.Dispose();
        }

        // https://stackoverflow.com/questions/5891538/listen-for-key-press-in-net-console-app#5891612
        private static void ReadKeys()
        {
            while (!Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;
                if (key.Key == ConsoleKey.E) _processor?.ToggleErrorSimulation();
                if (key.Key == ConsoleKey.S) _processor?.ToggleSleepMode();
            }
        }
    }
}


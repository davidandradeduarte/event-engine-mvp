using System;
using System.Threading.Tasks;

namespace MT_Processor
{
    public class Program
    {
        private static MassTransitConfigurator _service;

        static void Main(string[] args)
        {
            Console.WriteLine("Consumer here!");

            var taskKeys = new Task(ReadKeys);

            taskKeys.Start();

            using (_service = new MassTransitConfigurator())
            {
                _service.StartListening();

                Task.WaitAll(taskKeys);
            }
        }

        // https://stackoverflow.com/questions/5891538/listen-for-key-press-in-net-console-app#5891612
        private static void ReadKeys()
        {
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("Press 'e' anytime to simulate an error while processing the next message");
            Console.WriteLine("Press 's' anytime to toogle sleep mode");
            Console.WriteLine("");

            while (!Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;
                if (key.Key == ConsoleKey.E) _service?.ToggleErrorSimulation();
                if (key.Key == ConsoleKey.S) _service?.ToggleSleepMode();
            }
        }
    }
}


using System;
using System.Threading;
using System.Threading.Tasks;
using EventGenerator;
using MassTransit;
using MT_Common;

namespace MT_Generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to send a message");
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("Press 'L' anytime to toogle a loop sending messages");
            Console.WriteLine("");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(Settings.Rabbit.Uri, h =>
                {
                    h.Username(Settings.Rabbit.User);
                    h.Password(Settings.Rabbit.Pass);
                });
            });

            Console.WriteLine("Connecting to Rabit...");
            bus.Start();
            Console.WriteLine("Connected!");
            Console.WriteLine("");

            var pf = new PerformanceCounterAdapter(Settings.PerfCounters.InstanceName.Rabbit);

            var sender = new Send(bus, pf);

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;
                if (key.Key == ConsoleKey.L)
                {
                    Loop(sender);
                    continue;
                }

                sender.SendMessage();

            } while (true);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }

        private static void Loop(Send sender)
        {
            var thread = new Thread(() =>
            {
                while (true) sender.SendMessage();
            });

            thread.Start();

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.L)
                {
                    thread.Abort();
                    break;
                }
            } while (true);
        }
    }
}

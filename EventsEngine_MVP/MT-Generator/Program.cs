using System;
using EventGenerator;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MT_Common;

namespace MT_Generator
{
    using System.Diagnostics;

    public class Program
    {
        private static PerformanceCounter _avgSendMessageExecTime;
        static void Main(string[] args)
        {
            CreateCounters();

            Console.WriteLine("Hello!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to send a message");
            Console.WriteLine("Press 'q' anytime to quit");
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

            var sender = new Send(bus);

            var sw = new Stopwatch();
            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;

                sw.Start();

                sender.SendMessage();

                sw.Stop();
                _avgSendMessageExecTime.RawValue = sw.ElapsedMilliseconds;
                sw.Reset();

            } while (true);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
        private static void CreateCounters()
        {
            _avgSendMessageExecTime = new PerformanceCounter("Cascade Data Access COM", "SendMessage execution Time in ms", "RabbitMQ", false)
            {
                RawValue = 0
            };
        }
    }
}

using System;
using EventGenerator;
using MassTransit;
using MassTransit.RabbitMqTransport;
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

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;

                sender.SendMessage();

            } while (true);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}

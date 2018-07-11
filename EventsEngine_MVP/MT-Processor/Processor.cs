using System;
using MassTransit;
using MT_Common;

namespace MT_Processor
{
    public class Processor : IDisposable
    {
        private readonly IBusControl bus;
        private readonly CreatedFooConsumer consumer = new CreatedFooConsumer();

        public Processor()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(Settings.Rabbit.Uri, h =>
                {
                    h.Username(Settings.Rabbit.User);
                    h.Password(Settings.Rabbit.Pass);
                });

                sbc.ReceiveEndpoint(host, Settings.Rabbit.Queue, ep =>
                {
                    // ep.Handler<CreatedFooConsumer>(context => Console.Out.WriteLineAsync($" yololo Received: {context.Message}"));

                    //ep.Consumer<CreatedFooConsumer>();
                    ep.Instance(consumer);

                });
            });

            bus.Start();
            Console.WriteLine("");
            Console.WriteLine($"Listening for {nameof(CreatedFoo)} Events...");
            Console.WriteLine("");
        }

        public void ToggleErrorSimulation()
        {
            consumer.ToggleErrorSimulation();
        }

        public void ToggleSleepMode()
        {
            consumer.ToggleSleepMode();
        }

        public void Dispose()
        {
            bus.Stop();
        }
    }
}
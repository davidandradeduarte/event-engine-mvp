using System;
using System.Net;
using GreenPipes;
using MassTransit;
using MT_Common;

namespace MT_Processor
{
    public class MassTransitConfigurator : IDisposable
    {
        private readonly IBusControl bus;
        private readonly PayloadConsumer consumer = new PayloadConsumer();

        public MassTransitConfigurator()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(Settings.Rabbit.Uri, h =>
                {
                    h.Username(Settings.Rabbit.User);
                    h.Password(Settings.Rabbit.Pass);
                });

                sbc.ReceiveEndpoint(host, Settings.Rabbit.Queue, e =>
                {
                    // ep.Handler<CreatedFooConsumer>(context => Console.Out.WriteLineAsync($"Received: {context.Message}"));

                    // we register an instance so that we can send "sleep" "error" calls
                    // normaly, we would leave the consumer class instance management to the MassTransit framework
                    //ep.Consumer<PayloadConsumer>();
                    e.Instance(consumer);

                    //e.UseRateLimit(10, TimeSpan.FromSeconds(5));

                });

                sbc.ReceiveEndpoint(host, Settings.Rabbit.FaultQueue, e =>
                {
                    e.Consumer<GlobalFaultConsumer>();
                });
            });
        }

        public void StartListening()
        {
            bus.Start();
            Console.WriteLine("");
            Console.WriteLine($"Listening for {nameof(Payload)} Events...");
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
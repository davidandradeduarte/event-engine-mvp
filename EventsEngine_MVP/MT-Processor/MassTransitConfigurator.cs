﻿using System;
using System.Net;
using GreenPipes;
using MassTransit;
using MT_Common;

namespace MT_Processor
{
    public class MassTransitConfigurator : IDisposable
    {
        private readonly IBusControl bus;
        private readonly CreatedFooConsumer consumer = new CreatedFooConsumer();

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
                    // ep.Handler<CreatedFooConsumer>(context => Console.Out.WriteLineAsync($" yololo Received: {context.Message}"));

                    // we register an instance so that we can send "sleep" "error" calls
                    // normaly, we would leave the consumer class instance management to the MassTransit framework
                    //ep.Consumer<CreatedFooConsumer>();
                    e.Instance(consumer);

                    e.UseRateLimit(10, TimeSpan.FromSeconds(5));

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
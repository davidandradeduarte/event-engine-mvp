using System;
using MassTransit;
using MT_Common;

namespace EventGenerator
{
    public class Send
    {
        private int count;
        private readonly IBusControl bus;
        private readonly IPerfCounters counter;

        public Send(IBusControl bus, IPerfCounters counter)
        {
            this.bus = bus;
            this.counter = counter;
        }

        public void SendMessage()
        {
            count++;

            var foo = new CreatedFoo
            {
                Foo = count,
                Bar = "this is a brand new bar-siness",
                Timez = DateTime.UtcNow
            };

            Console.WriteLine("");
            Console.WriteLine($"Sending Message: '{foo}'");

            counter.Start();

            // run sync so that we can measure the actual time
            bus.Publish(foo).GetAwaiter().GetResult();

            counter.Stop();
        }
    }
}

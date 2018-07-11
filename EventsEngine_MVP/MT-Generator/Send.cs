using System;
using MassTransit;
using MT_Common;

namespace EventGenerator
{
    public class Send
    {
        private int count;
        private readonly IBusControl bus;

        public Send(IBusControl bus)
        {
            this.bus = bus;
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
            bus.Publish(foo);
        }
    }
}

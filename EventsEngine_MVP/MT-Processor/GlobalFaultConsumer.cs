using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MT_Common;

namespace MT_Processor
{
    public class GlobalFaultConsumer : IConsumer<Fault<Payload>>
    {
        public async Task Consume(ConsumeContext<Fault<Payload>> context)
        {
            var payload = context.Message;

            var originalMessage = payload.Message;

            var exceptions = payload.Exceptions;

            Console.WriteLine("");

            if (exceptions.Any(x => x.ExceptionType == "System.InvalidOperationException"))
            {
                Console.WriteLine($"{nameof(GlobalFaultConsumer)}: will retry message {originalMessage.Foo} ...");
                Console.WriteLine("");
                await context.Publish(originalMessage);
            }
            else
            {
                Console.WriteLine($"{nameof(GlobalFaultConsumer)}: will NOT retry message {originalMessage.Foo}!");
                Console.WriteLine("");
            }
        }
    }
}

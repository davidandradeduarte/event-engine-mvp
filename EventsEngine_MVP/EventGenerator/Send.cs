using System;
using System.Messaging;
using MT_Common;

namespace EventGenerator
{
    public class Send
    {
        private int count;
        private readonly IPerfCounters _counter;

        public Send(IPerfCounters counter)
        {
            _counter = counter;
        }

        public void SendMessage()
        {
            count++;

            var message = new Payload
            {
                Foo = count,
                Bar = $"This is a test message using MSMQ -- { count }",
                Timez = DateTime.UtcNow
            };

            if (!MessageQueue.Exists(Settings.Msmq.Path))
            {
                Console.WriteLine($"Creating queue: '{Settings.Msmq.Path}'");
                MessageQueue.Create(Settings.Msmq.Path);
            }

            _counter.Start();
            using (var messageQueue = new MessageQueue(Settings.Msmq.Path, QueueAccessMode.Send))
            {
                messageQueue.Label = "This is a test queue";
                messageQueue.Send(message);
            }
            _counter.Stop();

            Console.WriteLine("");
            Console.WriteLine($"Sent: '{message}'");
            Console.WriteLine($"Message {count} Sent.");
        }
    }
}

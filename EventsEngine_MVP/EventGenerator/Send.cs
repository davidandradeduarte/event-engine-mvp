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
            string message = $"This is a test message -- {count}";

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

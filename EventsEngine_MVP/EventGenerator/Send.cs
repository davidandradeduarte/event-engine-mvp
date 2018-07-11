using System;
using System.Messaging;

namespace EventGenerator
{
    public class Send
    {
        private int count;

        public void SendMessage()
        {
            count++;
            string message = $"This is a test message -- {count}";

            if (!MessageQueue.Exists(Constants.Queue.Path))
            {
                Console.WriteLine($"Creating queue: '{Constants.Queue.Path}'");
                MessageQueue.Create(Constants.Queue.Path);
            }

            using (var messageQueue = new MessageQueue(Constants.Queue.Path, QueueAccessMode.Send))
            {
                messageQueue.Label = "This is a test queue";
                messageQueue.Send(message);
            }

            Console.WriteLine("");
            Console.WriteLine($"Sent: '{message}'");
            Console.WriteLine($"Message {count} Sent.");
        }
    }
}

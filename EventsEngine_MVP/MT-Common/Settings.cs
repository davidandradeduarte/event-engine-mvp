using System;

namespace MT_Common
{
    public static class Settings
    {
        public static class Msmq
        {
            public const string Path = @".\Private$\mr-fields";
        }

        public static class Rabbit
        {
            // Default Rabbit management UI: http://localhost:15672

            public static readonly Uri Uri = new Uri("rabbitmq://localhost");
            public const string User = "guest";
            public const string Pass = "guest";
            public const string Queue = "test_queue";
            public const string FaultQueue = "test_queue_fault";
        }

        public static class PerfCounters
        {
            public const string CategoryName = "Cascade Data Access COM";
            public const string CounterName = "SendMessage execution Time in ms";
            public static class InstanceName
            {
                public const string Msmq = "MSMQ";
                public const string Rabbit = "RabbitMQ";
            }
        }
    }
}

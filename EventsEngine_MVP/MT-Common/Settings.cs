using System;

namespace MT_Common
{
    public static class Settings
    {
        public static class Rabbit
        {
            // Default Rabbit management UI: http://localhost:15672

            public static readonly Uri Uri = new Uri("rabbitmq://localhost");
            public const string User = "guest";
            public const string Pass = "guest";
            public const string Queue = "test_queue";
        }
    }
}

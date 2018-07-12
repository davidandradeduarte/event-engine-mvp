using System;

namespace EventGenerator
{
    using System.Diagnostics;

    class Program
    {
        private static PerformanceCounter _avgSendMessageExecTime;
        public static void Main(string[] args)
        {
            CreateCounters();

            var sender = new Send();

            Console.WriteLine("Hello!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to send a message");
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("");

            var sw = new Stopwatch();
            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;


                sw.Start();

                sender.SendMessage();

                sw.Stop();
                _avgSendMessageExecTime.RawValue = sw.ElapsedMilliseconds;
                sw.Reset();

            } while (true);

            Console.WriteLine("");
            Console.WriteLine("Kay. Bye!");
            Console.ReadKey();
        }

        private static void CreateCounters()
        {
            _avgSendMessageExecTime = new PerformanceCounter("Cascade Data Access COM", "SendMessage execution Time in ms", "MSMQ", false)
            {
                RawValue = 0
            };
        }
    }
}

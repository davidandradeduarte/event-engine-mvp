using System;
using System.Threading;
using MT_Common;

namespace EventGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pf = new PerformanceCounterAdapter(Settings.PerfCounters.InstanceName.Msmq);

            var sender = new Send(pf);

            Console.WriteLine("Hello!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to send a message");
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("Press 'L' anytime to toogle a loop sending messages");
            Console.WriteLine("");

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;
                if (key.Key == ConsoleKey.L)
                {
                    Loop(sender);
                    continue;
                }

                sender.SendMessage();

            } while (true);

            Console.WriteLine("");
            Console.WriteLine("Kay. Bye!");
            Console.ReadKey();
        }

        private static void Loop(Send sender)
        {
            var thread = new Thread(() =>
            {
                while (true) sender.SendMessage();
            });

            thread.Start();

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.L)
                {
                    thread.Abort();
                    break;
                }
            } while (true);
        }
    }
}

using System;

namespace EventGenerator
{
    class Program
    {

        public static void Main(string[] args)
        {
            var sender = new Send();

            Console.WriteLine("Hello!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to send a message");
            Console.WriteLine("Press 'q' anytime to quit");
            Console.WriteLine("");

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q) break;

                sender.SendMessage();

            } while (true);

            Console.WriteLine("");
            Console.WriteLine("Kay. Bye!");
            Console.ReadKey();
        }
    }
}

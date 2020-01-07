using System;

namespace Texas_hold_em
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Texas-hold-em! *****");
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Pass input in the form of: <5 board cards> <hand 1> <hand 2> <...> <hand N>. And press Enter.");
                var input = Console.ReadLine();
                var res = TexasHoldEmService.Evaluate(input);
                Console.WriteLine(res);

                Console.WriteLine("Press Esc to exit the console, or some other key to pass one more input");
                key = Console.ReadKey();
                Console.WriteLine();
            }
            while (key.Key != ConsoleKey.Escape);
            
        }
    }
}

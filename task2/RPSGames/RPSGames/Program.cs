using System;

namespace RPSGames
{
    class Program
    {
        static void Main()
        {
            // set default value
            int tableCount = 5;
            int winScore = 20;
            bool parseOk = false;

            while (!parseOk)
            {
                Console.WriteLine("Please enter player's count in team.");
                parseOk = Int32.TryParse(Console.ReadLine(), out tableCount);
            }

            parseOk = false;

            while (!parseOk)
            {
                Console.WriteLine("Please enter amount score to win.");
                parseOk = Int32.TryParse(Console.ReadLine(), out winScore);
            }

            Console.WriteLine("{0, 10}{1, 10}{2, 10}{3, 10}", "Pair # ",  Team.Red, Team.Blue, "Result");

            for (int i = 0; i < 100; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();

            Game game = new Game(tableCount, winScore);

            Console.ReadKey();
        }
    }
}

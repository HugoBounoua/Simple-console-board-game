using System;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game started...");
            Board.Init(10, 10);

            string input = string.Empty;
            while(input != "quit")
            {
                Board.Instance.Display();
                input = Console.ReadLine();
                if(input == "quit")
                {
                    Console.WriteLine("Game ended.");
                    input = Console.ReadLine();
                    return;
                }
                Board.Instance.NextStep();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Task_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            bool exitGame = false;
            Game game = new Game(120, 40);

            while (!exitGame)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(intercept: true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            game.MovePlayer(MOVE.UP);
                            break;
                        case ConsoleKey.DownArrow:
                            game.MovePlayer(MOVE.DOWN);
                            break;
                        case ConsoleKey.LeftArrow:
                            game.MovePlayer(MOVE.LEFT);
                            break;
                        case ConsoleKey.RightArrow:
                            game.MovePlayer(MOVE.RIGHT);
                            break;
                        case ConsoleKey.Escape:
                            exitGame = true;
                            break;
                    }
                }
            }
        }
    }
}

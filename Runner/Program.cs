using SimpleGame;
using System.Drawing;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            var rng = new Random();
            var boardSize = new Size(rng.Next(5, 10), rng.Next(5, 10));
            var finishPos = new Point(rng.Next(0, boardSize.Height - 1), boardSize.Width - 1);
            var playerInitialPos = new Point(rng.Next(0, boardSize.Height - 1), 0);
            var boardParams = new BoardParams(finishPos, boardSize);

            for (var i = 0; i < rng.Next(10, boardSize.Width * boardSize.Height / 3 * 2 - 2); i++)
            {
                // otestowac generowanie Punktu początkowego i Przeszkód
                var point = new Point(rng.Next(0, boardSize.Height - 1), rng.Next(0, boardSize.Width - 1));
                if (point != playerInitialPos)
                    boardParams.Obstacles.Add(point);
            }

            var game = new SimpleGame.SimpleGame(boardParams, playerInitialPos);
            game.Start();
        }
    }
}

// Sprawdzic 50 razy czy nie poleci InvalidOperationExcception "Cannot move through obstacles" podczas uruchamiania gry
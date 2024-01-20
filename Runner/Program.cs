using SimpleGame;
using System.Drawing;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var finishPos = new Point(2, 4);
            var playerInitialPos = new Point(2, 0);
            var boardSize = new Size(5, 5);
            var boardParams = new BoardParams(finishPos, boardSize);
            boardParams.Obstacles = new List<Point>()
            {
                new Point(2, 1),
                new Point(4, 1),
                new Point(1, 4),
                new Point(2, 3),
                new Point(3, 0),
                new Point(0, 3)
            };
            var game = new SimpleGame.SimpleGame(boardParams, playerInitialPos);
            game.Start();
        }
    }
}
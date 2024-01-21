using NUnit.Framework;
using System.Drawing;

namespace SimpleGame.Tests
{
    [TestFixture]
    public class GameTests
    {
        private SimpleGame _game;
        [OneTimeSetUp] 
        public void SetUp()
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

            _game = new SimpleGame(boardParams, playerInitialPos);
        }

        [Test]
        public void increases_error_counter_on_crossing_boundary()
        {
            var expectedHumanErrorsCount = 1;
            _game.Move(new Point(-1, 0));
            Assert.That(_game.HumanErrors == expectedHumanErrorsCount);
        }

        [Test]
        public void increases_error_counter_on_hitting_obstacle()
        {
            var expectedHumanErrorsCount = _game.HumanErrors + 1;
            var obs = _game.BoardParameters.Obstacles.FirstOrDefault();
            _game.Move(obs);
            Assert.That(_game.HumanErrors == expectedHumanErrorsCount);
        }

        [Test]
        public void game_end_on_reaching_finish_line()
        {
            var obs = _game.BoardParameters.FinishPosition;
            _game.Move(obs);
            Assert.That(_game.Finished);
        }
    }
}

using NUnit.Framework;
using System.Drawing;

namespace SimpleGame.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;
        [OneTimeSetUp]
        public void SetUp()
        {
            // Tests should not run with different input data every iteration, however to satisfy university tasksssss's needs & demands I use RNG to test this code.
            var rng = new Random();
            var boardSize = new Size(rng.Next(5, 10), rng.Next(5, 10));
            var finishPos = new Point(rng.Next(0, boardSize.Height - 1), boardSize.Width - 1);
            var playerInitialPos = new Point(rng.Next(0, boardSize.Height - 1), 0);
            var boardParams = new BoardParams(finishPos, boardSize);

            for (var i = 0; i < rng.Next(10, boardSize.Width * boardSize.Height / 3 * 2 - 2); i++)
            {
                // otestowac generowanie Punktu początkowego i Przeszkód
                var point = new Point(rng.Next(0, boardSize.Height - 1), rng.Next(0, boardSize.Width - 1));
                
                // The IF statement below should be contained in some business-logic-expressing entity and not in the place of Program's entry point.
                if (point != playerInitialPos)
                    boardParams.Obstacles.Add(point);
            }

            _board = new Board(boardParams);
        }

        [Test]
        public void can_reach_finish_point()
        {
            _board.PlayerPosition = _board.BoardParams.FinishPosition;
            Assert.That(_board.PlayerPosition == _board.BoardParams.FinishPosition);
        }

        [Test]
        public void cannot_enter_obstacle()
        {
            var obs = _board.BoardParams.Obstacles.FirstOrDefault();
            var ex = Assert.Catch<InvalidOperationException>(() => { _board.PlayerPosition = obs; });
            Assert.That(ex.Message.Equals(ErrorsUtil.EX_INVALID_OPERATION_OBSTACLE));
        }

        [Test]
        public void displays_proper_exception_message_about_crossing_boundary()
        {
            var boundary = new Point(-1, 0);
            var ex = Assert.Catch<InvalidOperationException>(() => { _board.PlayerPosition = boundary; });
            Assert.That(ex.Message.Equals(ErrorsUtil.EX_INVALID_OPERATION_BOUNDARY));
        }

        [Test]
        public void cant_leave_board_from_left()
        {
            Assert.Throws<InvalidOperationException>(() => { _board.PlayerPosition = new Point(-1, 0); });
        }

        [Test]
        public void cant_leave_board_from_top()
        {
            Assert.Throws<InvalidOperationException>(() => { _board.PlayerPosition = new Point(0, -1); });
        }

        [Test]
        public void cant_leave_board_from_right()
        {
            Assert.Throws<InvalidOperationException>(() => { _board.PlayerPosition = new Point(_board.BoardParams.Size.Width + 1, 0); });
        }

        [Test]
        public void cant_leave_board_from_bottom()
        {
            Assert.Throws<InvalidOperationException>(() => { _board.PlayerPosition = new Point(0, _board.BoardParams.Size.Height + 1); });
        }

        [Test]
        public void obstacles_exist()
        {
            Assert.That(_board.BoardParams.Obstacles.Any());
        }
    }
}

using System.Drawing;

namespace SimpleGame
{
    public class SimpleGame
    {
        public event Action GameFinished;

        public Point PlayerPosition { get; private set; }
        public BoardParams BoardParameters { get; init; }

        private readonly Board _board;
        private bool _finished;

        public SimpleGame(BoardParams boardParams, Point playerPosition)
        {
            BoardParameters = boardParams;
            PlayerPosition = playerPosition;

            _board = new(boardParams)
            {
                MovablePosition = new Point(2, 0)
            };
            _board.FinishReached += B_FinishReached;

            Start();
        }

        public void Start()
        {
            _board.Print();
            while (!_finished)
            {
                var val = Console.ReadKey();
                Console.Clear();
                switch (val.Key.ToString())
                {
                    case "LeftArrow":
                        MoveLeft();
                        break;
                    case "RightArrow":
                        MoveRight();
                        break;
                    case "UpArrow":
                        MoveUp();
                        break;
                    case "DownArrow":
                        MoveDown();
                        break;
                }
                _board.Print();
            }
        }

        public void MoveDown()
        {
            _board.MovablePosition = new Point(_board.MovablePosition.X + 1, _board.MovablePosition.Y);
        }

        public void MoveUp()
        {
            _board.MovablePosition = new Point(_board.MovablePosition.X - 1, _board.MovablePosition.Y);
        }

        public void MoveRight()
        {
            _board.MovablePosition = new Point(_board.MovablePosition.X, _board.MovablePosition.Y + 1);
        }

        public void MoveLeft()
        {
            _board.MovablePosition = new Point(_board.MovablePosition.X, _board.MovablePosition.Y - 1);
        }

        void B_FinishReached()
        {
            _finished = true;
            GameFinished?.Invoke();
        }
    }
}

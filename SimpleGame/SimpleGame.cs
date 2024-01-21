using System.Drawing;

namespace SimpleGame
{
    public class SimpleGame
    {
        private const string KEY_ARROW_LEFT = "LeftArrow";
        private const string KEY_ARROW_RIGHT = "RightArrow";
        private const string KEY_ARROW_UP = "UpArrow";
        private const string KEY_ARROW_DOWN = "DownArrow";
        private const string MESSAGE_ERRORS_NUM_2 = " errors so far.";
        private const string MESSAGE_ERRORS_NUM_1 = "You've made ";
        private readonly Board _board;
        public bool Finished { get; private set; } = false;
        
        public event Action GameFinished;
        public Point PlayerPosition { get; private set; }
        public BoardParams BoardParameters { get; init; }
        public int HumanErrors { get; private set; }

        public SimpleGame(BoardParams boardParams, Point playerPosition)
        {
            BoardParameters = boardParams;
            PlayerPosition = playerPosition;

            _board = new(boardParams)
            {
                PlayerPosition = playerPosition
            };
            _board.FinishReached += B_FinishReached;
        }

        public void Start()
        {
            _board.Print();
            while (!Finished)
            {
                Console.WriteLine($"{MESSAGE_ERRORS_NUM_1}{HumanErrors}{MESSAGE_ERRORS_NUM_2}");
                var input = Console.ReadKey();
                Console.Clear();
                var point = new Point();
                switch (input.Key.ToString())
                {
                    case KEY_ARROW_LEFT:
                        point = new Point(_board.PlayerPosition.X, _board.PlayerPosition.Y - 1);
                        break;
                    case KEY_ARROW_RIGHT:
                        point = new Point(_board.PlayerPosition.X, _board.PlayerPosition.Y + 1);
                        break;
                    case KEY_ARROW_UP:
                        point = new Point(_board.PlayerPosition.X - 1, _board.PlayerPosition.Y);
                        break;
                    case KEY_ARROW_DOWN:
                        point = new Point(_board.PlayerPosition.X + 1, _board.PlayerPosition.Y);
                        break;
                }
                Move(point);
                _board.Print();
            }
        }

        public void Move(Point pos)
        {
            try
            {
                _board.PlayerPosition = pos;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Equals(ErrorsUtil.EX_INVALID_OPERATION_BOUNDARY)
                    || ex.Message.Equals(ErrorsUtil.EX_INVALID_OPERATION_OBSTACLE))
                    HumanErrors++;
                else
                    throw;
            }
        }

        void B_FinishReached()
        {
            Finished = true;
            GameFinished?.Invoke();
        }
    }
}

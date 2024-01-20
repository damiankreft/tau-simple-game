using System.Drawing;

namespace SimpleGame;

public class Board : IMovableBoard
{
    private Point _movablePosition;
    private readonly char[,] _board;
    
    public event Action FinishReached;
    public BoardParams BoardParams { get; init; }
    public Point MovablePosition
    {
        get
        {
            return _movablePosition;
        }

        set
        {
            _board[_movablePosition.X, _movablePosition.Y] = Pictograms.FREE;
            _movablePosition = value;
            _board[_movablePosition.X, _movablePosition.Y] = Pictograms.PLAYER;

            if (_movablePosition == BoardParams.FinishPosition)
            {
                FinishReached?.Invoke();
            }
        }
    }

    public Board(BoardParams boardParams)
    {
        BoardParams = boardParams;
        _board = new char[BoardParams.Size.Height, BoardParams.Size.Width];
        Initialize();

        // test size and positions
        _board[BoardParams.FinishPosition.X, BoardParams.FinishPosition.Y] = Pictograms.FINISH;
    }

    private void Initialize()
    {
        for (var row = 0; row < BoardParams.Size.Height; row++)
        {
            for (var col = 0; col < BoardParams.Size.Width; col++)
            {
                _board[row, col] = Pictograms.FREE;
            }
        }

        AddObstacles();
    }

    private void AddObstacles()
    {
        foreach (var o in BoardParams.Obstacles)
        {
            _board[o.X, o.Y] = Pictograms.OBSTACLE;
        }
    }

    public void Print()
    {
        for (var row = 0; row < BoardParams.Size.Height; row++)
        {
            for (var col = 0; col < BoardParams.Size.Width; col++)
            {
                Console.Write(_board[row, col] + " ");
            }

            Console.WriteLine();
        }
    }
}

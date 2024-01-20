using System.Drawing;

namespace SimpleGame;

public class Board
{
    public Point StartPosition { get; private set; }
    public Point EndPosition { get; private set; }
    public Size Size { get; private set; }

    private readonly char[,] _board;

    public Board(Size size, Point startPosition, Point endPosition)
    {
        Size = size;
        StartPosition = startPosition;
        EndPosition = endPosition;

        _board = new char[size.Height, size.Width];

        Initialize();
        Print();
    }

    private void Initialize()
    {
        for (var row = 0; row < Size.Height; row++)
        {
            for (var col = 0; col < Size.Width; col++)
            {
                _board[row, col] = '@';
            }
        }
    }

    private void Print()
    {
        for (var row = 0; row < Size.Height; row++)
        {
            for (var col = 0; col < Size.Width; col++)
            {
                Console.Write(_board[row, col] + " ");
            }

            Console.WriteLine();
        }
    }
}

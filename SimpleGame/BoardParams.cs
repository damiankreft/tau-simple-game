using System.Drawing;

namespace SimpleGame
{
    public class BoardParams
    {
        public Point FinishPosition { get; init; }
        public Size Size { get; init; }
        public List<Point> Obstacles { get; set; }

        public BoardParams(Point finishPosition, Size size)
        {
            FinishPosition = finishPosition;
            Size = size;
            Obstacles = new List<Point>();
        }
    }
}

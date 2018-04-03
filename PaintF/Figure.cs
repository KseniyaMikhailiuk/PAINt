using System.Drawing;

namespace PaintF
{
    public abstract class Figure
    {
        public Pen Pen { get; set; }
        public Point StartPoint { get; set; }
        public Point FinishPoint { get; set; }
        public abstract void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint);
    }
}

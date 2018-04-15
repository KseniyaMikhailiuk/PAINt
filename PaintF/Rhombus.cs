using System.Runtime.Serialization;
using System.Drawing;

namespace PaintF
{
    class Rhombus: Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            Point[] point = { StartPoint, FinishPoint, new Point (StartPoint.X, FinishPoint.Y + (FinishPoint.Y - StartPoint.Y)),
                                                       new Point (StartPoint.X - (FinishPoint.X - StartPoint.X), FinishPoint.Y)};
            g.DrawPolygon(pen, point);
        }
    }
}

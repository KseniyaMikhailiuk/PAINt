using System;
using System.Drawing;
using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    public class Rhombus: Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            Point[] point = { StartPoint, FinishPoint, new Point (StartPoint.X, FinishPoint.Y + (FinishPoint.Y - StartPoint.Y)),
                                                       new Point (StartPoint.X - (FinishPoint.X - StartPoint.X), FinishPoint.Y)};
            g.DrawPolygon(pen, point);
        }

        public override bool IsPointIn(Point point)
        {
            Point centre = new Point(StartPoint.X, FinishPoint.Y);
            int halfHeight = Math.Abs(FinishPoint.Y - StartPoint.Y);
            int halfWidth = Math.Abs(FinishPoint.X - StartPoint.X);
            return (halfHeight * Math.Abs(point.X - centre.X) + halfWidth *
                Math.Abs(point.Y - centre.Y) <= halfWidth * halfHeight);
        }

        public override object Clone()
        {
            return (Rhombus)MemberwiseClone();
        }
    }
}

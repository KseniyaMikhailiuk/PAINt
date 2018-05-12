using System;
using System.Drawing;

namespace PaintF
{
    public class Ellipse: Figure
    {
        private Point Centre { get; set; }

        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = FinishPoint.Y - StartPoint.Y;
            int Width = FinishPoint.X - StartPoint.X;
            g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Height);
            Centre = new Point(StartPoint.X + Width / 2, StartPoint.Y + Height / 2);
        }

        public override bool IsPointIn(Point point)
        {
            int Height = FinishPoint.Y - StartPoint.Y;
            int Width = FinishPoint.X - StartPoint.X;
            Point newPointCoordinates = new Point(point.X - Centre.X, point.Y - Centre.Y);
            return ((Math.Pow(newPointCoordinates.X, 2) / Math.Pow(Width / 2, 2) +
                (Math.Pow(newPointCoordinates.Y, 2)) / Math.Pow(Height / 2, 2)) < 1);
        }

        public override object Clone()
        {
            return (Ellipse)MemberwiseClone();
        }
    }
}

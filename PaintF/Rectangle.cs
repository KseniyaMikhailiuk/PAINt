using System.Runtime.Serialization;
using System.Drawing;
using System;


namespace PaintF
{
    class Rectangle : Figure
    { 
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = Math.Abs(FinishPoint.Y - StartPoint.Y);
            int Width = Math.Abs(FinishPoint.X - StartPoint.X);
            if (FinishPoint.Y < StartPoint.Y)
            {
                StartPoint = new Point(StartPoint.X, FinishPoint.Y);
            }
            if (FinishPoint.X < StartPoint.X)
            {
                StartPoint = new Point(FinishPoint.X, StartPoint.Y);
            }
            g.DrawRectangle(pen, StartPoint.X, StartPoint.Y, Width, Height);
        }

        public override bool IsPointIn(Point point)
        {
            if (((point.X >= StartPoint.X) && (point.X <= FinishPoint.X))
                && ((point.Y <= FinishPoint.Y) && (point.Y >= StartPoint.Y)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object Clone()
        {
            return (Rectangle)MemberwiseClone();
        }
    }
}

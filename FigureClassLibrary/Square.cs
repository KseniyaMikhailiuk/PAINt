using System;
using System.Drawing;

namespace PaintF
{
    public class Square: Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Width = Math.Abs(FinishPoint.X - StartPoint.X);

            if ((FinishPoint.Y < StartPoint.Y) && (FinishPoint.X < StartPoint.X))
            {
                StartPoint = new Point(FinishPoint.X, StartPoint.Y - Width);
            }
            else
            {
                if ((FinishPoint.Y < StartPoint.Y) && (FinishPoint.X > StartPoint.X))
                {
                    StartPoint = new Point(StartPoint.X, StartPoint.Y - Width);
                }
                else
                {
                    if (FinishPoint.X < StartPoint.X)
                    {
                        StartPoint = new Point(FinishPoint.X, StartPoint.Y);
                    }
                }
            }
            g.DrawRectangle(pen, StartPoint.X, StartPoint.Y, Width, Width);
        }

        public override bool IsPointIn(Point point)
        {
            int Width = FinishPoint.X - StartPoint.X;
            return (((point.X >= StartPoint.X) && (point.X <= StartPoint.X + Width))
                && ((point.Y <= StartPoint.Y + Width) && (point.Y >= StartPoint.Y)));
        }

        public override object Clone()
        {
            return (Square)MemberwiseClone();
        }
    }
}

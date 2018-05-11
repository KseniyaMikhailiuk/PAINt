using System.Drawing;
using System;

namespace FigureClassLibrary
{
    public class Circle : Figure
    {
        private Point Centre { get; set; }

        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Width = FinishPoint.X - StartPoint.X;
            int Height;
            if (IsIn1or3quarter(Width))
            {
                Height = -Width;
            }
            else
            {
                Height = Width;
            }
            g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Height);
            Centre = new Point(StartPoint.X + Width / 2, StartPoint.Y + Height / 2);
        }

        private bool IsIn1or3quarter(int Width)
        {
            return (((Width > 0) && (FinishPoint.Y < StartPoint.Y)) || ((Width < 0) && (FinishPoint.Y > StartPoint.Y)));
        }

        public override bool IsPointIn(Point point)
        {
            int Width = FinishPoint.X - StartPoint.X;
            double distance = Math.Sqrt(Math.Pow((point.X - Centre.X), 2) + Math.Pow((point.Y - Centre.Y), 2));
            return (distance <= Width / 2);
        }

        public override object Clone()
        {
            return (Circle)MemberwiseClone();
        }
    }
}

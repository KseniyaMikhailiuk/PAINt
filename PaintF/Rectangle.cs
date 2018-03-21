using System.Windows.Forms;
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
    }
}

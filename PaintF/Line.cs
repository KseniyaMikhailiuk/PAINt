using System.Drawing;
using System;
namespace PaintF
{
    public class Line: Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            g.DrawLine(pen, StartPoint, FinishPoint);
        }

        public override bool IsPointIn(Point point)
        {
            float k = (float)(FinishPoint.Y - StartPoint.Y) / (FinishPoint.X - StartPoint.X);
            float b = -(StartPoint.X * FinishPoint.Y - FinishPoint.X * StartPoint.Y) / (FinishPoint.X - StartPoint.X);
            int start = StartPoint.X;
            int finish = FinishPoint.X;
            if (StartPoint.X > FinishPoint.X)
            {
                start = FinishPoint.X;
                finish = StartPoint.X;
            }
            for (int i = start; i <= finish; i++)
            {
                if (((point.Y <= Math.Round(i * k + b) + Pen.Width + 3) &&
                    (point.Y >= Math.Round(i * k + b) - Pen.Width - 3)) && 
                    ((point.X <= i + Pen.Width + 3) && (point.X >= i - Pen.Width - 3)))
                {
                    return true;
                }
            }
            return false;
        }

        public override object Clone()
        {
            return (Line)MemberwiseClone();
        }
    }
}

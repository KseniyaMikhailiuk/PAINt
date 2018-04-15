using System.Drawing;
using System.Runtime.Serialization;



namespace PaintF
{
    class Circle: Ellipse
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Width = FinishPoint.X - StartPoint.X;
            if (((Width > 0) && (FinishPoint.Y < StartPoint.Y)) || ((Width < 0) && (FinishPoint.Y > StartPoint.Y)))
            {
                g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, -Width);
            }
            else
            {
                g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Width);
            }
        }
    }
}

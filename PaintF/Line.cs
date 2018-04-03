using System.Drawing;

namespace PaintF
{
    public class Line: Figure
    {
        public override void Draw(Graphics g, System.Drawing.Pen pen, Point StartPoint, Point FinishPoint)
        {
            g.DrawLine(pen, StartPoint, FinishPoint);
        }
    }
}

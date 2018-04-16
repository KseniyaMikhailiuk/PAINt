using System.Drawing;
using Newtonsoft.Json;

namespace PaintF
{
    public class Line: Figure
    {
        JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            g.DrawLine(pen, StartPoint, FinishPoint);
        }

        public override bool IsPointIn(Point point)
        {
            float k = (FinishPoint.Y - StartPoint.Y) / (FinishPoint.X - StartPoint.X);
            float b = -(StartPoint.X * FinishPoint.Y - FinishPoint.X * StartPoint.Y) / (FinishPoint.X - StartPoint.X);
            int start;
            int finish;
            if (StartPoint.X < FinishPoint.X)
            {
                start = StartPoint.X;
                finish = FinishPoint.X;
            }
            else
            {
                start = FinishPoint.X;
                finish = StartPoint.X;
            }
            for (int i = start; i <= finish; i++)
            {
                if ((point.Y == i * k + b) && ((point.X <= i + 20) && (point.X >= i - 20)))
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

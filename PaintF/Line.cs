using System.Drawing;
using Newtonsoft.Json;

namespace PaintF
{
    public class Line: Figure
    {
        JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public override void Draw(Graphics g, System.Drawing.Pen pen, Point StartPoint, Point FinishPoint)
        {
            g.DrawLine(pen, StartPoint, FinishPoint);
        }
    }
}

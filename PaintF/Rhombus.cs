using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Rhombus: Figure
    {
        public override Figure Create()
        {
            return new Rhombus();
        }
        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            Point[] point = { StartPoint, FinishPoint, new Point (StartPoint.X, FinishPoint.Y + (FinishPoint.Y - StartPoint.Y)), new Point (StartPoint.X - (FinishPoint.X - StartPoint.X), FinishPoint.Y)};
            e.Graphics.DrawPolygon(pen, point);
        }
    }
}

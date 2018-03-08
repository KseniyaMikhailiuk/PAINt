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
            Point[] point = { X, Y, new Point (X.X, Y.Y + (Y.Y - X.Y)), new Point (X.X - (Y.X - X.X), Y.Y)};
            e.Graphics.DrawPolygon(pen, point);
        }
    }
}

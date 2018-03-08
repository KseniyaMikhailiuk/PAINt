using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Ellipse: Rectangle
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawEllipse(pen, X.X, X.Y, getFinishPointX(), getFinishPointY());
        }
    }
}

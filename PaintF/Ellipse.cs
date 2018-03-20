using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Ellipse: Rectangle
    {
        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, getFinishPointX(), getFinishPointY());
        }
    }
}

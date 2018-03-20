using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Circle: Ellipse
    {
        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, getFinishPointX(), getFinishPointX());
        }
    }
}

using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Circle: Ellipse
    {
        public override Figure Create()
        {
            return new Circle();
        }

        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawEllipse(pen, X.X, X.Y, getFinishPointX(), getFinishPointX());
        }
    }
}

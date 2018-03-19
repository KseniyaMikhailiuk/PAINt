using System.Windows.Forms;
using System.Drawing;
using System;

namespace PaintF
{
    class Square: Rectangle
    {
        public override Figure Create()
        {
            return new Square();
        }

        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawRectangle(pen, StartPoint.X, StartPoint.Y, getFinishPointX(), getFinishPointX());
        }
    }
}

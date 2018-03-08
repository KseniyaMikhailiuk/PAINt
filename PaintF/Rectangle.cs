using System.Windows.Forms;
using System.Drawing;
using System;


namespace PaintF
{
    class Rectangle : Figure
    {
        public override Figure Create()
        {
            return new Rectangle();
        }

        public int getFinishPointX()
        {
            return Math.Abs(Y.X - X.X);
        }

        public int getFinishPointY()
        {
            return Math.Abs(Y.Y - X.Y);
        }

        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawRectangle(pen, X.X, X.Y, getFinishPointX(), getFinishPointY());
        }
    }
}

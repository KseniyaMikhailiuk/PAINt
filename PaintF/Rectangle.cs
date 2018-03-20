using System.Windows.Forms;
using System.Drawing;
using System;


namespace PaintF
{
    class Rectangle : Figure
    {
        public int getFinishPointX()
        {
            return Math.Abs(FinishPoint.X - StartPoint.X);
        }

        public int getFinishPointY()
        {
            return Math.Abs(FinishPoint.Y - StartPoint.Y);
        }

        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawRectangle(pen, StartPoint.X, StartPoint.Y, getFinishPointX(), getFinishPointY());
        }
    }
}

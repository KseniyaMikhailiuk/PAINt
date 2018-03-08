using System.Windows.Forms;
using System.Drawing;

namespace PaintF
{
    public class Line: Figure
    {
        public override Figure Create()
        {
            return new Line();
        }
        public override void Draw(object sender, PaintEventArgs e, Pen pen)
        {
            e.Graphics.DrawLine(pen, X, Y);
        }
    }
}

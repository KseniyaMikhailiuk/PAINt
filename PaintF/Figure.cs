using System.Drawing;
using System.Windows.Forms;

namespace PaintF
{
    public abstract class Figure
    {
        public Point StartPoint { get; set; }
        public Point FinishPoint { get; set; }
        public abstract void Draw(object sender, PaintEventArgs e, Pen pen);
    }
}

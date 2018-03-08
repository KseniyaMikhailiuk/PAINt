using System.Drawing;
using System.Windows.Forms;

namespace PaintF
{
    public class Figure
    {

        public Point X { get; set; }
        public Point Y { get; set; }
        public virtual Figure Create() { return new Figure(); }
        public virtual void Draw(object sender, PaintEventArgs e, Pen pen) {}
    }
}

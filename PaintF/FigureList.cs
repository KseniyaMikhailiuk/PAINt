using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintF
{
    public class FigureList
    {
        public List<Figure> Figures = new List<Figure> {
            new Line(),
            new Square(),
            new Circle(),
            new Ellipse(),
            new Rhombus(),
            new Rectangle()
        };
    }
}

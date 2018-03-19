using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintF
{
    public class FigureCreatorList
    {
        public List<FigureCreator> Creators = new List<FigureCreator> {
            new LineCreator(),
            new SquareCreator(),
            new CircleCreator(),
            new EllipseCreator(),
            new RhombusCreator(),
            new RectangleCreator()
        };
    }
}

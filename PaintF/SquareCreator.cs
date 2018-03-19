using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintF
{
    class SquareCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Square();
        }
    }
}

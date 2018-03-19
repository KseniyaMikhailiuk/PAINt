using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintF
{
    class CircleCreator:FigureCreator
    {
        public override Figure Create()
        {
           return new Circle();
        }
    }
}

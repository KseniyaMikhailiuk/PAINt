using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    class CircleCreator:FigureCreator
    {
        public override Figure Create()
        {
           return new Circle();
        }
    }
}

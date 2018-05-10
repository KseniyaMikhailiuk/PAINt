using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    class EllipseCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}

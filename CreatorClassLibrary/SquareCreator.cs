using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    class SquareCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Square();
        }
    }
}

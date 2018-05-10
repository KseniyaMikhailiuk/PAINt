using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    class LineCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Line();
        }
    }
}

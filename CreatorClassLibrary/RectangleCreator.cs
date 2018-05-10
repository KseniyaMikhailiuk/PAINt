using AbstractFigureClassLibrary;

namespace FigureClassLibrary
{
    class RectangleCreator:FigureCreator
    {
        public override Figure Create()
        {
            return new Rectangle();
        }
    }
}

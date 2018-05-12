using PaintF;

namespace CreatorClassLibrary
{
    public class SquareCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Square();
        }
    }
}

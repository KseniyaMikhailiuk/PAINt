using AbstractClassLibrary;

namespace SquareClassLibrary
{
    public class SquareCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Square();
        }
    }
}

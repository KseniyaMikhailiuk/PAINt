using PaintF;

namespace CreatorClassLibrary
{
    public class LineCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Line();
        }
    }
}

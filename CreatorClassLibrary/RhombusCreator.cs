using PaintF;

namespace CreatorClassLibrary
{
    public class RhombusCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Rhombus();
        }
    }
}

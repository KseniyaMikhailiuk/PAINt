using FigureClassLibrary;

namespace PaintF
{
    public class RhombusCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Rhombus();
        }
    }
}

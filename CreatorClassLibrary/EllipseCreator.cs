using PaintF;

namespace CreatorClassLibrary
{
    public class EllipseCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}

using AbstractClassLibrary;

namespace EllipseClassLibrary
{
    public class EllipseCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}

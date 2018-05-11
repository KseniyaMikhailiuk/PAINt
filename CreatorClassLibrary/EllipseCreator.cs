namespace FigureClassLibrary
{
    public class EllipseCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}

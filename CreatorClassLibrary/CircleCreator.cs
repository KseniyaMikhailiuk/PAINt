using PaintF;

namespace CreatorClassLibrary
{
    public class CircleCreator:FigureCreator
    {
        public override Figure Create()
        {
           return new Circle();
        }
    }
}

using AbstractClassLibrary;

namespace RectangleClassLibrary
{
    public class RectangleCreator:FigureCreator
    {
        public override Figure Create()
        {
            return new Rectangle();
        }
    }
}

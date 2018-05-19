using AbstractClassLibrary;

namespace RhombusClassLibrary
{
    public class RhombusCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Rhombus();
        }
    }
}

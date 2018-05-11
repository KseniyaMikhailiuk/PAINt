namespace FigureClassLibrary
{
    public class LineCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Line();
        }
    }
}

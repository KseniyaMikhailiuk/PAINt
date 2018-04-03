namespace PaintF
{
    class CircleCreator:FigureCreator
    {
        public override Figure Create()
        {
           return new Circle();
        }
    }
}

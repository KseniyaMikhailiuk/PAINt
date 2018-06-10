using System.Collections.Generic;
using System.Drawing;
using AbstractClassLibrary;

namespace PaintF
{
    public class UserFigure: Figure
    {
        public static int FieldWidth;
        public static int FieldHeight;
        public List<Figure> UserFigureList = new List<Figure>();

        public override bool IsPointIn(Point point)
        {
            return true;
        }

        public override object Clone()
        {
            UserFigure clonedFigure = new UserFigure();
            foreach (var figure in UserFigureList)
            {
                clonedFigure.UserFigureList.Add((Figure)figure.Clone());
            }
            return clonedFigure;
        }

        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            foreach (var figure in UserFigureList)
            {
                if (figure != null)
                {
                    float widthDif = (FinishPoint.X - StartPoint.X) / (float)FieldWidth;
                    figure.StartPoint = CountStartPoint(figure, widthDif);
                    figure.FinishPoint = CountFinishPoint(figure, widthDif);
                    figure.Draw(g, figure.Pen, figure.StartPoint, figure.FinishPoint);
                }
            }
        }

        public Point CountStartPoint(Figure figure, float widthDif)
        {
            int tempStartX = (int)(StartPoint.X + figure.FixedStartPoint.X * widthDif);
            int tempStartY = (int)(StartPoint.Y + figure.FixedStartPoint.Y * widthDif);
            return new Point(tempStartX, tempStartY);
        }

        public Point CountFinishPoint(Figure figure, float widthDif)
        {
            int tempFinishX = (int)(StartPoint.X + figure.FixedFinishPoint.X * widthDif);
            int tempFinishY = (int)(StartPoint.Y + figure.FixedFinishPoint.Y * widthDif);
            return new Point(tempFinishX, tempFinishY);
        }

        public override void Add(List<Figure> list)
        {
            foreach (var figure in UserFigureList)
            {
                figure.FixedFinishPoint = figure.FinishPoint;
                figure.FixedStartPoint = figure.StartPoint;
                list.Add(figure);
            }
        }
    }
}

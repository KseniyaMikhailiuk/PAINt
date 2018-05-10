using System.Drawing;

namespace AbstractFigureClassLibrary
{
    public interface ISelectable
    {
        bool IsPointIn(Point point);
    }
}

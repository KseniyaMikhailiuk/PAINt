using System.Drawing;

namespace FigureClassLibrary
{
    public interface ISelectable
    {
        bool IsPointIn(Point point);
    }
}

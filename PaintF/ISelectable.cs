using System.Drawing;

namespace PaintF
{
    public interface ISelectable
    {
        bool IsPointIn(Point point);
    }
}

using System.Drawing;
namespace IPluginFigure
{
    public interface IPlugin
    {
        void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint);
    }
}

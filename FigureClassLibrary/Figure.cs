using System.Drawing;
using System.Runtime.Serialization;
using System;
using IPluginFigure;

namespace PaintF
{
    [Serializable]
    [DataContract]
    public abstract class Figure : ISelectable, ICloneable, IPlugin
    {
        private Pen pen;

        public Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                pen = value;
                penColor = pen.Color;
                penWidth = pen.Width;
            }
        }

        [DataMember]
        private Color penColor;
        [DataMember]
        private float penWidth;

        [OnDeserialized()]
        internal void Reinitialize(StreamingContext context)
        {
            Pen = new Pen(penColor, penWidth);
        }

        public abstract bool IsPointIn(Point point);

        public abstract object Clone();

        public void kek()
        {

        }

        [DataMember]
        public Point StartPoint { get; set; }
        [DataMember]
        public Point FinishPoint { get; set; }
        public abstract void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint);
    }
}

using System.Drawing;
using System.Runtime.Serialization;
using System;

namespace PaintF
{
    [DataContract]
    public abstract class Figure : ISelectable, ICloneable
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

        [DataMember]
        public Point StartPoint { get; set; }
        [DataMember]
        public Point FinishPoint { get; set; }
        public abstract void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint);
    }
}

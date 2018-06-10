using System.Drawing;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace AbstractClassLibrary
{
    [Serializable]
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

        public virtual void Add(List<Figure> list)
        {
            list.Add(this);
        }

        [DataMember]
        public Point StartPoint { get; set; }
        [DataMember]
        public Point FinishPoint { get; set; }

        [DataMember]
        public Point FixedStartPoint { get; set; }
        [DataMember]
        public Point FixedFinishPoint { get; set; }
        public abstract void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint);
    }
}

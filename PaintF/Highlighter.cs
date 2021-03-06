﻿using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using AbstractClassLibrary;

namespace PaintF
{
    public class Highlighter
    {
        public Figure HighlightObject;
        public Figure SelectedFigure { get; set; }

        public void Start(Point point, List<Figure> list, PictureBox pictureBox)
        {
            SelectedFigure = RecognizeFigure(point, list);
            if (SelectedFigure != null)
            {
                HighlightObject = (Figure)SelectedFigure.Clone();
                Highlight(HighlightObject, pictureBox);
            }
        }

        public void Highlight(Figure highlightObject, PictureBox pictureBox)
        {
            Graphics g = pictureBox.CreateGraphics();
            highlightObject.Pen = SystemPens.Highlight;
            highlightObject.Draw(g, highlightObject.Pen, highlightObject.StartPoint, highlightObject.FinishPoint);
        }

        public Figure RecognizeFigure(Point point, List<Figure> list) 
        {
             foreach (var figure in list)
            {
                if (figure.IsPointIn(point))
                {
                    return figure;
                }
            }
            return null;
        }
    }
}

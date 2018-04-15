﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintF
{
    public partial class Paint : Form
    {
        FigureList figureList = new FigureList();

        FigureCreatorList figureCreatorList = new FigureCreatorList();

        FigureCreator figureCreator;

        Figure figure;

        Color penColor = Color.Black;
        int penWidth = 1;

        Pen pen = new Pen(Color.Black, 1);

        public bool isClicked = false;

        public struct MenuItemInfo
        {
            public string figureName;
            public string creatorType;
            public FigureCreator FigureCreator;
        }

        Point X;
        Point Y;

        public Paint()
        {
            InitializeComponent();

            Color[] colorArray = new Color[] { Color.Black, Color.Blue, Color.Orange, Color.Green,
                                               Color.Gold, Color.Indigo, Color.HotPink, Color.LimeGreen, Color.Red };

            int[] widthArray = new int[] { 1, 2, 3, 4, 5 };

            MenuItemInfo[] itemsArray = new MenuItemInfo[] {
                new MenuItemInfo { figureName = "Line", creatorType = (typeof(LineCreator)).ToString(), FigureCreator = new LineCreator() },
                new MenuItemInfo { figureName = "Rectangle", creatorType = (typeof(RectangleCreator)).ToString(), FigureCreator = new RectangleCreator()},
                new MenuItemInfo { figureName = "Square", creatorType = (typeof(SquareCreator)).ToString(), FigureCreator = new SquareCreator()},
                new MenuItemInfo { figureName = "Rhombus", creatorType = (typeof(RhombusCreator)).ToString(), FigureCreator = new RhombusCreator()},
                new MenuItemInfo { figureName = "Circle", creatorType = (typeof(CircleCreator)).ToString(), FigureCreator = new CircleCreator()},
                new MenuItemInfo { figureName = "Ellipce", creatorType = (typeof(EllipseCreator)).ToString(), FigureCreator = new EllipseCreator()}
            };

            ToolStripMenuItem menuItem;

            foreach (MenuItemInfo items in itemsArray)
            {
                menuItem = new ToolStripMenuItem(items.figureName)
                {
                    Tag = items.FigureCreator
                };
                menuItem.Click += new EventHandler(MenuItemFigureClickHandler);
                figuresToolStripMenuItem.DropDownItems.Add(menuItem);
            }

            foreach (Color color in colorArray)
            {
                menuItem = new ToolStripMenuItem()
                {
                    BackColor = color
                };
                menuItem.Click += new EventHandler(MenuItemColorClickHandler);
                colorToolStripMenuItem.DropDownItems.Add(menuItem);
            }

            foreach (int width in widthArray)
            {
                menuItem = new ToolStripMenuItem()
                {
                    Tag = width,
                    Text = Convert.ToString(width)
                };
                menuItem.Click += new EventHandler(MenuItemWidthClickHandler);
                widthToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }

        private void MenuItemFigureClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            figureCreator = (FigureCreator)clickedItem.Tag;
        }

        private void MenuItemColorClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            penColor = clickedItem.BackColor;
            pen = new Pen(penColor, penWidth);
        }

        private void MenuItemWidthClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            penWidth = (int)clickedItem.Tag;
            pen = new Pen(penColor, penWidth);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (figureCreator != null)
            {
                figure = figureCreator.Create();
                figure.Pen = pen;
                isClicked = true;
                X = new Point(e.X, e.Y);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
            if (figure != null)
            {
                figureList.Figures.Add(figure);
            }
        }


        public void RepaintFigureList(Graphics g)
        {
            if (figureList.Figures.Count > 0)
            {
                foreach (var fig in figureList.Figures)
                {
                    fig.Draw(g, fig.Pen, fig.StartPoint, fig.FinishPoint);
                }
            }
        }

        public void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (figure != null)
            {
                figure.StartPoint = X;
                figure.FinishPoint = Y;
                figure.Draw(e.Graphics, figure.Pen, figure.StartPoint, figure.FinishPoint);
                RepaintFigureList(e.Graphics);
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                Y = new Point(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void paintAllFiguresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Figure allFigurePainter;
            int startX = 80;
            int startY = 20;
            int finishX = 160;
            int finishY = startY + 60;

            foreach (var creator in figureCreatorList.Creators)
            {
                allFigurePainter = creator.Create();
                allFigurePainter.StartPoint = new Point(startX, startY);
                allFigurePainter.FinishPoint = new Point(finishX, finishY);
                allFigurePainter.Pen = pen;
                allFigurePainter.Draw(g, allFigurePainter.Pen, allFigurePainter.StartPoint, allFigurePainter.FinishPoint);
                if (allFigurePainter != null)
                {
                    figureList.Figures.Add(allFigurePainter);
                }
                startY += 100;
                finishY = startY + 50;
                RepaintFigureList(g);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serializer = new Serializer(); 
            if (figureList.Figures.Count != 0)
            {
                serializer.Serialize(figureList.Figures);
            }
            else
            {
                string message = "Нарисуйте что-нибудь, тогда сохраним))";
                string caption = "Save";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSurface();
            var deserializer = new Serializer();
            figureList.Figures = deserializer.Deserialize();
            Graphics g = pictureBox1.CreateGraphics();
            RepaintFigureList(g);
        }

        public void ClearSurface()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            figureList.Figures.Clear();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSurface();
        }
    }
}
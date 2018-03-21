using System;
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

        Pen pen = new Pen(Color.Black, 3);

        public bool isClicked = false;

        public struct MenuItemInfo
        {
            public string figureName;
            public string creatorType;
        }

        Point X;
        Point Y;
        
        public Paint()
        {
            InitializeComponent();


            MenuItemInfo[] itemsArray = new MenuItemInfo[] {
                new MenuItemInfo { figureName = "Line", creatorType = (typeof(LineCreator)).ToString() },
                new MenuItemInfo { figureName = "Rectangle", creatorType = (typeof(RectangleCreator)).ToString()},
                new MenuItemInfo { figureName = "Square", creatorType = (typeof(SquareCreator)).ToString()},
                new MenuItemInfo { figureName = "Rhombus", creatorType = (typeof(RhombusCreator)).ToString()},
                new MenuItemInfo { figureName = "Circle", creatorType = (typeof(CircleCreator)).ToString()},
                new MenuItemInfo { figureName = "Ellipce", creatorType = (typeof(EllipseCreator)).ToString()}
            };

            ToolStripMenuItem menuItem;

            for (int i = 0; i < itemsArray.Length; i++)
            {
                menuItem = new ToolStripMenuItem(itemsArray[i].figureName);
                menuItem.Tag = itemsArray[i].creatorType;
                menuItem.Click += new EventHandler(MenuItemClickHandler);
                figuresToolStripMenuItem.DropDownItems.Add(menuItem);
            }

        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            foreach (var creator in figureCreatorList.Creators)
            {
                if ((string)clickedItem.Tag == creator.ToString())
                {
                    figureCreator = creator;
                }
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            figure = figureCreator.Create();
            isClicked = true;
            X = new Point(e.X, e.Y);
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
            if (figure != null)
            {
                figureList.Figures.Add(figure);
            }
        }

        public void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (figure != null)
            {
                figure.StartPoint = X;
                figure.FinishPoint = Y;

                figure.Draw(e.Graphics, pen, figure.StartPoint, figure.FinishPoint);
                if (figureList.Figures.Count > 0)
                {
                    foreach (var fig in figureList.Figures)
                    {
                        fig.Draw(e.Graphics, pen, fig.StartPoint, fig.FinishPoint);
                    }
                }
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
            int startX = 80;
            int startY = 20;
            int finishX = 160;
            int finishY = startY + 60;

            foreach (var creator in figureCreatorList.Creators)
            {
                figureCreator = creator;
                figure = figureCreator.Create();
                figure.StartPoint = new Point(startX, startY);
                figure.FinishPoint = new Point(finishX, finishY);
                figure.Draw(g, pen, figure.StartPoint, figure.FinishPoint);
                if (figure != null)
                {
                    figureList.Figures.Add(figure);
                }
                startY += 100;
                finishY = startY + 50;
                if (figureList.Figures.Count > 0)
                {
                    foreach (var fig in figureList.Figures)
                    {
                        fig.Draw(g, pen, fig.StartPoint, fig.FinishPoint);
                    }
                }
            }
        }

    }
}
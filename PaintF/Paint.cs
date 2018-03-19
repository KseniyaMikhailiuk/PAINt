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


        Point X;
        Point Y;
        
        public Paint()
        {
            InitializeComponent();
            var menuItem = new ToolStripMenuItem("Line");
            menuItem.Tag = (typeof(LineCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);

            menuItem = new ToolStripMenuItem("Rectangle");
            menuItem.Tag = (typeof(RectangleCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);

            menuItem = new ToolStripMenuItem("Square");
            menuItem.Tag = (typeof(SquareCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);

            menuItem = new ToolStripMenuItem("Rhombus");
            menuItem.Tag = (typeof(RhombusCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);

            menuItem = new ToolStripMenuItem("Circle");
            menuItem.Tag = (typeof(CircleCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);

            menuItem = new ToolStripMenuItem("Ellipce");
            menuItem.Tag = (typeof(EllipseCreator)).ToString();
            menuItem.Click += new EventHandler(MenuItemClickHandler);
            figuresToolStripMenuItem.DropDownItems.Add(menuItem);
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

                figure.Draw(sender, e, pen);
                if (figureList.Figures.Count > 0)
                {
                    foreach (var fig in figureList.Figures)
                    {
                        fig.Draw(sender, e, pen);
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

    }
}

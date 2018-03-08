using System;
using System.Drawing;
using System.Windows.Forms;


namespace PaintF
{
    public partial class Paint : Form
    {
        FigureList figureList = new FigureList();

        Figure figure;
        Pen pen = new Pen(Color.Black, 20);

        public bool isClicked = false;

        bool isFirstFigurePainted = false; 

        Point X;
        Point Y;
        
        public Paint()
        {
            InitializeComponent();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isFirstFigurePainted)
            {
                Figure temp = figure.Create();
                figure = temp;
            }
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
                
                figure.X = X;
                figure.Y = Y;

                isFirstFigurePainted = true;
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

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Line();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                Y = new Point(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Rectangle();
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Square();
        }

        private void rhombusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Rhombus();
        }

        private void ellipceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Ellipse();
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstFigurePainted = false;
            figure = new Circle();
        }

    }
}

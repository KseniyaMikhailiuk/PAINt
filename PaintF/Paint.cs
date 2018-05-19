using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using AbstractClassLibrary;
using System.Collections.Generic;

namespace PaintF
{
    public partial class Paint : Form
    {
        FigureList FigureList = new FigureList();

        FigureCreatorList FigureCreatorList = new FigureCreatorList();

        FigureCreator FigureCreator;

        Figure Figure;
        Figure CopiedFigure;

        Color penColor = Color.Black;
        int penWidth = 1;

        Pen pen = new Pen(Color.Black, 1);
        Highlighter Highlighter = new Highlighter() { IsHighlighted = false};

        public bool IsPastButtonPressed = false;
        public bool IsItFirstPast = false;
        public bool isClicked = false;
        public bool IsHighlighterOn = false;

        public struct MenuItemInfo
        {
            public string figureName;
            public string creatorType;
            public FigureCreator FigureCreator;
        }

        Point StartPoint;
        Point FinishPoint;

        public Paint()
        {
            AddPlugins();
            InitializeComponent();

            MenuItem[] menuItems = new MenuItem[] {new MenuItem("Delete", new EventHandler(ContextMenuDeleteClickHandler)),
                                                   new MenuItem("Copy", new EventHandler(ContextMenuCopyClickHandler)),
                                                   new MenuItem("Paste", new EventHandler(ContextMenuPasteClickHandler))};
            ContextMenu = new ContextMenu(menuItems);
            List<MenuItemInfo> itemsList = new List<MenuItemInfo>();
            string[] FigureNames = new string[] { "Line", "Rectangle", "Square", "Rhombus", "Circle", "Ellipse" };
            foreach (var creator in FigureCreatorList.Creators)
            {
                foreach (var figurename in FigureNames)
                {
                    if ((creator).ToString().Contains(figurename))
                    {
                        itemsList.Add(new MenuItemInfo
                        {
                            figureName = figurename,
                            creatorType = (creator).ToString(),
                            FigureCreator = creator
                        });
                        break;
                    }
                }
            }

            ToolStripMenuItem menuItem;

            foreach (MenuItemInfo items in itemsList)
            {
                menuItem = new ToolStripMenuItem(items.figureName)
                {
                    Tag = items.FigureCreator
                };
                menuItem.Click += new EventHandler(MenuItemFigureClickHandler);
                figuresToolStripMenuItem.DropDownItems.Add(menuItem);
            }
            
        }

        private void ContextMenuDeleteClickHandler(object sender, EventArgs e)
        {
            if ((Highlighter.SelectedFigure != null) && (IsHighlighterOn))
            {
                FigureList.Figures.Remove(Highlighter.SelectedFigure);
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(pictureBox1.BackColor);
                RepaintFigureList(g);
            }
        }

        private void ContextMenuCopyClickHandler(object sender, EventArgs e)
        {
            if ((Highlighter.SelectedFigure != null) && (IsHighlighterOn))
            {
                CopiedFigure = (Figure)Highlighter.SelectedFigure.Clone();
                IsItFirstPast = true;
            }
        }

        private void ContextMenuPasteClickHandler(object sender, EventArgs e)
        {
            if ((IsHighlighterOn) && (CopiedFigure != null))
            {
                if (!IsItFirstPast)
                {
                    CopiedFigure = (Figure)CopiedFigure.Clone();
                }
                IsPastButtonPressed = true;
            }
        }

        private void MenuItemFigureClickHandler(object sender, EventArgs e)
        {
            if (IsHighlighterOn)
            {
                IsHighlighterOn = false;
                Highlighter.IsHighlighted = false;
            }

            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            FigureCreator = (FigureCreator)clickedItem.Tag;
        }

        private void MenuColorClickHandler(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            penColor = colorDialog1.Color;
            pen = new Pen(penColor, penWidth);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsHighlighterOn)
            {
                Graphics g = pictureBox1.CreateGraphics();
                RepaintFigureList(g);
                Highlighter.Start(new Point(e.X, e.Y), FigureList.Figures, pictureBox1, Highlighter.IsHighlighted);
            }
            else
            {
                if (FigureCreator != null)
                {
                    Figure = FigureCreator.Create();
                    Figure.Pen = pen;
                    isClicked = true;
                    StartPoint = new Point(e.X, e.Y);
                }
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                isClicked = false;
                if (Figure != null)
                {
                    FigureList.Figures.Add(Figure);
                }
            }
        }


        public void RepaintFigureList(Graphics g)
        {
            if (FigureList.Figures.Count > 0)
            {
                foreach (var fig in FigureList.Figures)
                {
                    fig.Draw(g, fig.Pen, fig.StartPoint, fig.FinishPoint);
                }
            }
        }

        public void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (Figure != null)
            {
                Figure.StartPoint = StartPoint;
                Figure.FinishPoint = FinishPoint;
                Figure.Draw(e.Graphics, Figure.Pen, Figure.StartPoint, Figure.FinishPoint);
                RepaintFigureList(e.Graphics);
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsHighlighterOn)
            {
                Cursor = Cursors.Hand;
            }

            if (isClicked)
            {
                FinishPoint = new Point(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serializer = new Serializer(); 
            if (FigureList.Figures.Count != 0)
            {
                serializer.Serialize(FigureList.Figures);
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
            FigureList.Figures = deserializer.Deserialize();
            Graphics g = pictureBox1.CreateGraphics();
            RepaintFigureList(g);
        }

        public void ClearSurface()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            FigureList.Figures.Clear();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSurface();
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsHighlighterOn = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsPastButtonPressed)
            {
                Point temp = CopiedFigure.StartPoint;
                CopiedFigure.StartPoint = new Point(e.X, e.Y); 
                CopiedFigure.FinishPoint = new Point(CopiedFigure.StartPoint.X + Math.Abs((temp.X - CopiedFigure.FinishPoint.X)),
                                                    CopiedFigure.StartPoint.Y + Math.Abs((temp.Y - CopiedFigure.FinishPoint.Y)));
                FigureList.Figures.Add(CopiedFigure);
                Graphics g = pictureBox1.CreateGraphics();
                RepaintFigureList(g);
                IsPastButtonPressed = false;
                IsItFirstPast = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            penWidth = trackBar1.Value;
            pen = new Pen(penColor, penWidth);
        }

        public void AddPlugins()
        {   
            String AddInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);   
            var AddInAssemblies = Directory.EnumerateFiles(AddInDir, "*Library.dll");

            foreach (var ass in AddInAssemblies)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(ass);
                    Type[] types = assembly.GetExportedTypes();
                    foreach (var type in types)
                    {
                        if (type.IsClass && type.GetTypeInfo().BaseType == typeof(FigureCreator) && !type.IsAbstract)
                        {
                            var plugin = Activator.CreateInstance(type);
                            FigureCreatorList.Creators.Add((FigureCreator)plugin);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    string caption = "Error";
                    MessageBox.Show(ex.Message, caption, button);
                }
            }     
        }
    }
}
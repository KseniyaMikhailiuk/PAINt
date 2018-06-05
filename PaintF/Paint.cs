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

        internal static Color PenColor = Color.Black;
        internal static int PenWidth = 1;



        Pen Pen;
        Highlighter Highlighter = new Highlighter() { IsHighlighted = false};

        public bool IsPastButtonPressed = false;
        public bool IsItFirstPast = false;
        public bool isClicked = false;
        public bool IsHighlighterOn = false;

        public struct StripMenuItemInfo
        {
            public string figureName;
            public FigureCreator FigureCreator;
        }

        public static Color BackgroundColor = Color.White;


        Point StartPoint;
        Point FinishPoint;

        public Paint()
        {
            AddPlugins();
            InitializeComponent();
            ConfigKeeper.LoadXml();


            List<StripMenuItemInfo> itemsList = new List<StripMenuItemInfo>();

            string[][] FigureNames = new string[][] { new string[]{ "Line", "Rectangle", "Square", "Rhombus", "Circle", "Ellipse" },
                                                        new string[]{ "Линия", "Прямоугольник", "Квадрат", "Ромб", "Круг", "Эллипс" } };

            MenuItem[]  menuItems = new MenuItem[] {new MenuItem("Delete", new EventHandler(ContextMenuDeleteClickHandler)),
                                                   new MenuItem("Copy", new EventHandler(ContextMenuCopyClickHandler)),
                                                   new MenuItem("Paste", new EventHandler(ContextMenuPasteClickHandler)),
                                                   new MenuItem("Background Color", new EventHandler(ContextMenuBGColorClickHandler))};

            ContextMenu = new ContextMenu(menuItems);



            foreach (var creator in FigureCreatorList.Creators)
            {
                foreach (var figurename in FigureNames[0])
                {
                    if ((creator).ToString().Contains(figurename))
                    {
                        itemsList.Add(new StripMenuItemInfo
                        {
                            figureName = figurename,
                            FigureCreator = creator
                        });
                        break;
            }
                }
            }

            ToolStripMenuItem menuItem;

            foreach (StripMenuItemInfo items in itemsList)
            {
                menuItem = new ToolStripMenuItem(items.figureName)
                {
                    Tag = items.FigureCreator
                };
                menuItem.Click += new EventHandler(MenuItemFigureClickHandler);
                figuresToolStripMenuItem.DropDownItems.Add(menuItem);
            }
            Pen = new Pen(PenColor, PenWidth);
            trackBar1.Value = PenWidth;
            pictureBox1.BackColor = BackgroundColor;
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

        private void ContextMenuBGColorClickHandler(object sender, EventArgs e)
        {
            isClicked = false;
            colorDialog1.ShowDialog();
            BackgroundColor = colorDialog1.Color;
            pictureBox1.BackColor = BackgroundColor;
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

        internal void MenuItemFigureClickHandler(object sender, EventArgs e)
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
            PenColor = colorDialog1.Color;
            Pen = new Pen(PenColor, PenWidth);
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
                if (FigureCreator != null && e.Button == MouseButtons.Left)
                {
                    Figure = FigureCreator.Create();
                    Figure.Pen = Pen;
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

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
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


        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSurface();
        }

        private void HighlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsHighlighterOn = true;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
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

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            PenWidth = trackBar1.Value;
            Pen = new Pen(PenColor, PenWidth);
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

        private void Paint_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigKeeper.SaveXml();
        }
    }
}
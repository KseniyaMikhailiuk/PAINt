using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;


namespace PaintF
{
    public class ConfigKeeper
    {
        public static void SaveXml()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration XmlDec = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(XmlDec);
            XmlElement xRoot = xdoc.CreateElement("settings");
            xdoc.AppendChild(xRoot);

            XmlElement settingElem = xdoc.CreateElement("setting");

            XmlElement penWidthElem = xdoc.CreateElement("penWidth");
            XmlElement penColorElem = xdoc.CreateElement("penColor");
            XmlElement BGcolorElem = xdoc.CreateElement("BGcolor");

            XmlText penWidthText = xdoc.CreateTextNode(Paint.PenWidth.ToString());
            XmlText penColorText = xdoc.CreateTextNode(Paint.PenColor.ToArgb().ToString());
            XmlText BGcolorText = xdoc.CreateTextNode(Paint.BackgroundColor.ToArgb().ToString());

            penWidthElem.AppendChild(penWidthText);
            penColorElem.AppendChild(penColorText);
            BGcolorElem.AppendChild(BGcolorText);

            settingElem.AppendChild(penWidthElem);
            settingElem.AppendChild(penColorElem);
            settingElem.AppendChild(BGcolorElem);

            xRoot.AppendChild(settingElem);

            xdoc.Save("prevSessionConfig.xml");
        }

        public static void LoadXml()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("prevSessionConfig.xml");
                XmlElement xmlRoot = xmlDocument.DocumentElement;

                foreach (XmlNode xnode in xmlRoot)
                {
                    foreach(XmlNode xnodechild in xnode)
                    {
                        // получаем атрибут name
                        if (xnodechild.Name == "penWidth")
                        {
                            if (xnodechild.FirstChild != null)
                            {
                                Paint.PenWidth = Int32.Parse(xnodechild.FirstChild.Value);
                            }
                        }
                        if (xnodechild.Name == "penColor")
                        {
                            if (xnodechild.FirstChild != null)
                            {
                                int rgbColor = Int32.Parse(xnodechild.FirstChild.Value);
                                Paint.PenColor = Color.FromArgb(rgbColor);
                            }
                        }
                        if (xnodechild.Name == "BGcolor")
                        {
                            if (xnodechild.FirstChild != null)
                            {
                                int rgbColor = Int32.Parse(xnodechild.FirstChild.Value);
                                Paint.BackgroundColor = Color.FromArgb(rgbColor);
                            }

                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
    }
}

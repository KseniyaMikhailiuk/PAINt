using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using AbstractClassLibrary;


namespace PaintF
{
    public class Serializer
    {
        public void Serialize(List<Figure> list)
        {
            string data = JsonConvert.SerializeObject(list, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            string readyString = Convert.ToBase64String(Encoding.Default.GetBytes(data));           
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                Stream fileWriter;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((fileWriter = saveFileDialog.OpenFile()) != null)
                    {
                        fileWriter.Write(Encoding.Default.GetBytes(readyString), 0, readyString.Length);
                        string message = "Аминь";
                        string caption = "bless and save";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show(message, caption, buttons);
                        fileWriter.Close();
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

        public List<Figure> Deserialize()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            Stream fileReader = null;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((fileReader = openFileDialog.OpenFile()) != null)
                {
                    return GetFigureListFromFile(fileReader);
                }
            }
            return Paint.FigureList.Figures;
        }

        public static List<Figure> GetFigureListFromFile(Stream fileReader)
        {
            try
            {
                List<Figure> figures = new List<Figure> { };
                using (fileReader)
                {
                    string data = "";
                    byte[] temp = new byte[1024];
                    int size;
                    while ((size = fileReader.Read(temp, 0, temp.Length)) > 0)
                    {
                        Array.Resize(ref temp, size);
                        data += Encoding.Default.GetString(temp);
                    }
                    byte[] result = Convert.FromBase64String(data);
                    figures = JsonConvert.DeserializeObject<List<Figure>>(Encoding.Default.GetString(result),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                    fileReader.Close();
                }
                return figures;
            }
            catch(Exception ex)
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                string caption = "Error";
                MessageBox.Show(ex.Message, caption, button);
                return Paint.FigureList.Figures;
            }
        }
    }
}

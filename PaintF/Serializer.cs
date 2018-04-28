using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;


namespace PaintF
{
    public class Serializer
    {
        string fileName = "figures.txt";

        public void Serialize(List<Figure> list)
        {
            string data = JsonConvert.SerializeObject(list, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            string readyString = Convert.ToBase64String(Encoding.Default.GetBytes(data));           
            try
            {
                var fileStream = new FileStream(fileName, FileMode.Create);
                fileStream.Write(Encoding.Default.GetBytes(readyString), 0, readyString.Length);
                string message = "Аминь";
                string caption = "bless and save";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                fileStream.Close();
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
            try
            {
                var fileStream = new StreamReader(File.Open(fileName, FileMode.Open));
                string data = fileStream.ReadToEnd();
                byte[] result = Convert.FromBase64String(data);
                string kek = Encoding.Default.GetString(result);
                List<Figure> figures = JsonConvert.DeserializeObject<List<Figure>>(Encoding.Default.GetString(result), 
                    new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                fileStream.Close();
                return figures;
            }
            catch (Exception ex)
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                string caption = "Error";
                MessageBox.Show(ex.Message, caption, button);
                List<Figure> figures = new List<Figure> { };
                return figures;
            }
        }
    }
}

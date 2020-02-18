using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace Movie_Guide
{
    public partial class AddNewFile : UserControl
    {
        bool exist = false;
        Add form = new Add();
        public AddNewFile()
        {
            InitializeComponent();

            bunifuMaterialTextbox3.Text = "";

            XmlDocument doc = new XmlDocument();
            doc.Load("Files.xml");
            XmlNodeList list = doc.GetElementsByTagName("File");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList children = list[i].ChildNodes;
                comboBox1.Items.Add(children[0].InnerText);

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            string file_name = bunifuMaterialTextbox3.Text;

            XmlDocument doc = new XmlDocument();
            doc.Load("Files.xml");
            XmlNodeList list = doc.GetElementsByTagName("File");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList children = list[i].ChildNodes;
                if (file_name == children[0].InnerText)
                {
                    exist = true;
                    break;
                }

            }


            if (exist)
            {
                MessageBox.Show("This File Already Exist");
                exist = false;
            }
            else
            {
                //add the file name to Files xml
                XmlDocument doc2 = new XmlDocument();
                XmlElement file = doc2.CreateElement("File");
                XmlElement node = doc2.CreateElement("name");
                node.InnerText = file_name;
                file.AppendChild(node);

                node = doc2.CreateElement("path");
                node.InnerText = "E:\\\\MovieGuide\\\\MovieGuide\\\\bin\\Debug\\\\" + file_name + ".xml";
                file.AppendChild(node);

                doc2.Load("Files.xml");
                XmlElement root = doc2.DocumentElement;
                root.AppendChild(file);
                doc2.Save("Files.xml");

                //create the new xml file
                XmlWriter write = XmlWriter.Create(file_name + ".xml");
                write.WriteStartDocument();

                write.WriteStartElement("Movies");
            
                write.WriteEndElement();

                write.WriteEndDocument();
                write.Close();
                MessageBox.Show("File Added Successfully");
                comboBox1.Items.Add(file_name);
            }




        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           string filename = comboBox1.SelectedItem.ToString() ;
           
            ////fdfdfdfdfdf



            //string path2 = "";

            //XmlDocument fileDoc2 = new XmlDocument();
            //fileDoc2.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Files.xml");
            //foreach (XmlNode node in fileDoc2.SelectNodes("Files/File"))
            //{
            //    if (comboBox1.Text == node.SelectSingleNode("name").InnerText)
            //    {
            //        path2 = node.SelectSingleNode("path").InnerText;
            //    }
            //}

            //XmlDocument del = new XmlDocument();
            //del.Load(path2);

            //XmlNodeList movie = del.GetElementsByTagName("Movie");
            //for (int i = 0; i < movie.Count; i++)
            //{
            //    XmlNodeList data = movie[i].ChildNodes;
            //    string movie_name = data[1].InnerText;

            //    XmlDocument allmovies = new XmlDocument();
            //    allmovies.Load("Allmovies.xml");

            //    XmlNodeList movies = del.GetElementsByTagName("Movie");
            //    for (int j = 0; j < movies.Count; i++)
            //    {

            //        XmlNodeList data2 = movies[i].ChildNodes;
            //        MessageBox.Show(movie_name + "  " + data2[1].InnerText);

            //        if (movie_name == data2[1].InnerText)
            //        {
            //            MessageBox.Show("sddddddd");

            //            movieClass mo = new movieClass();
            //            mo.deleteMovie(movie_name, "E:\\\\MovieGuide\\\\MovieGuide\\\\bin\\\\Debug" + filename + ".xml");
            //        }


            //    }


            //}

            update_files(filename);
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            XmlDocument doc3 = new XmlDocument();
            doc3.Load("Files.xml");
            XmlNodeList list2 = doc3.GetElementsByTagName("File");
            for (int i = 0; i < list2.Count; i++)
            {
                XmlNodeList children = list2[i].ChildNodes;
                comboBox1.Items.Add(children[0].InnerText);

            }

        }
        private void update_files(string filename)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Files.xml");
            //F:\projects\c#\Movie Guide\Movie Guide\Movie Guide\bin\Debug
            //  XmlNodeList dlist = doc.GetElementsByTagName("name");

            MessageBox.Show(filename);
            foreach (XmlNode node in doc.SelectNodes("Files/File"))
            {
                String file_name = node.SelectSingleNode("name").InnerText;
                file_name += ".xml";
               // String file_path = node.SelectSingleNode("path").InnerText;
                //MessageBox.Show(filename);
                if (file_name.Equals(filename))
                {
                    MessageBox.Show("file found");
                  //  File.Delete(file_path);
                    node.ParentNode.RemoveChild(node);
                    doc.Save("Files.xml");

                }
            }

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
using Bunifu.Framework.UI;

namespace Movie_Guide
{
    public partial class Add : UserControl
    {
        string photo="";
        public Add()
        {

            InitializeComponent();
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox1.Text = "";
            Update_files();
            comboBox3.Items.Clear();


            XmlDocument doc = new XmlDocument();
            doc.Load("Files.xml");
            XmlNodeList list = doc.GetElementsByTagName("File");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList children = list[i].ChildNodes;
                comboBox3.Items.Add(children[0].InnerText);

            }

        }



        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                photo = ofd.FileName;
            }
        }

        public void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (photo == "")
            {
                MessageBox.Show("please you have to add a poster to the movie");
            }
            else
            {
                string geners = "";
                string filename = comboBox3.SelectedItem + ".xml";
                if (File.Exists(filename))
                {
                    if (load_newM(bunifuMaterialTextbox2.Text, filename))
                    {

                        XmlDocument doc = new XmlDocument();
                        XmlElement movie = doc.CreateElement("Movie");

                        XmlElement node = doc.CreateElement("Id");
                        node.InnerText = bunifuMaterialTextbox3.Text;
                        movie.AppendChild(node);

                        node = doc.CreateElement("Title");
                        node.InnerText = bunifuMaterialTextbox2.Text;
                        movie.AppendChild(node);

                        node = doc.CreateElement("Director");
                        node.InnerText = bunifuMaterialTextbox1.Text;
                        movie.AppendChild(node);

                        node = doc.CreateElement("Year");
                        node.InnerText = comboBox1.SelectedItem.ToString();
                        movie.AppendChild(node);
                        node = doc.CreateElement("Genres");
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            if (checkedListBox1.GetItemChecked(i))
                            {
                                geners += (string)checkedListBox1.Items[i] + ",";


                            }
                        }
                        node.InnerText = geners;
                        movie.AppendChild(node);


                        node = doc.CreateElement("Rate");
                        node.InnerText = comboBox2.SelectedItem.ToString();
                        movie.AppendChild(node);

                        node = doc.CreateElement("Poster");
                        node.InnerText = photo;
                        movie.AppendChild(node);


                        doc.Load(comboBox3.SelectedItem + ".xml");
                        XmlElement root = doc.DocumentElement;
                        root.AppendChild(movie);

                        doc.Save(comboBox3.SelectedItem + ".xml");
                        MessageBox.Show("Movie is added successfully");

                        load_directors(bunifuMaterialTextbox1.Text);
                        load_directors_movies(bunifuMaterialTextbox1.Text, bunifuMaterialTextbox2.Text);
                        load_directors_rate(bunifuMaterialTextbox1.Text, comboBox2.SelectedItem.ToString());


                        doc.Load("Allmovies.xml");
                        XmlElement root2 = doc.DocumentElement;
                        root2.AppendChild(movie);
                        doc.Save("Allmovies.xml");
                    }
                    else
                    {
                        MessageBox.Show("the movie already exists you can update it");
                        Update_files();
                    }

                }
                else
                {
                    MessageBox.Show("this file do not exist");
                }
            }
        }


        private void load_directors(string text)
        {
            String Director_name = text;
            if (!File.Exists("Director.xml"))
            {
                XmlWriter writer = XmlWriter.Create("Director.xml");
                writer.WriteStartDocument();
                writer.WriteStartElement("Table");
                writer.WriteAttributeString("name", "Directors");

                writer.WriteStartElement("Director");
                writer.WriteStartElement("Name");
                writer.WriteString(Director_name);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            else
            {
                if (load_newD(text))
                {
                    XmlDocument dir_doc = new XmlDocument();
                    XmlElement Director = dir_doc.CreateElement("Director");
                    XmlElement dir_node = dir_doc.CreateElement("Name");
                    dir_node.InnerText = Director_name;
                    Director.AppendChild(dir_node);
                    dir_doc.Load("Director.xml");
                    XmlElement dir_root = dir_doc.DocumentElement;
                    dir_root.AppendChild(Director);
                    dir_doc.Save("Director.xml");
                }
                else
                    MessageBox.Show("the director already exists");
            }
            MessageBox.Show("a director is right");
        }

        private bool load_newD(string text)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Director.xml");
            XmlNodeList dlist = doc.GetElementsByTagName("Name");
            for (int i = 0; i < dlist.Count; i++)
            {
                String dvalue = dlist[i].InnerText;
                if (dvalue.Equals(text))
                {
                    return false;
                }
            }
            return true;
        }
        private void load_directors_movies(string Director_name, string Movie_name)
        {
            string dfile = "";
            if (!File.Exists("DirectorsMovies.xml"))
            {
                XmlWriter writer = XmlWriter.Create("DirectorsMovies.xml");
                writer.WriteStartDocument();
                writer.WriteStartElement("Table");
                writer.WriteAttributeString("name", "Directors");

                writer.WriteStartElement("Director_Movie");
                writer.WriteStartElement("Director");
                writer.WriteString(Director_name);
                writer.WriteEndElement();

                writer.WriteStartElement("Movie_Name");
                writer.WriteString(Movie_name);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();


            }
            else
            {
                dfile = "DirectorsMovies.xml";
                if (load_newMD(Director_name, dfile))
                {
                    XmlDocument dir_doc = new XmlDocument();
                    XmlElement Director = dir_doc.CreateElement("Director_Movie");
                    XmlElement dir_node = dir_doc.CreateElement("Director");
                    dir_node.InnerText = Director_name;
                    Director.AppendChild(dir_node);
                    dir_node = dir_doc.CreateElement("Movie_Name");
                    dir_node.InnerText = Movie_name;
                    Director.AppendChild(dir_node);
                    dir_doc.Load(dfile);
                    XmlElement dir_root = dir_doc.DocumentElement;
                    dir_root.AppendChild(Director);
                    dir_doc.Save(dfile);
                    MessageBox.Show("new director with movies is added");

                }
                else
                {
                   // MessageBox.Show("hghfghfgfddfgdf");
                    XmlDocument doc = new XmlDocument();
                    doc.Load(dfile);

                    foreach (XmlNode node in doc.SelectNodes("Table/Director_Movie"))
                    {
                        if (node.SelectSingleNode("Director").InnerText == Director_name)
                        {
                            MessageBox.Show("Director found ");
                            string s = node.SelectSingleNode("Movie_Name").InnerText;
                            node.ParentNode.RemoveChild(node);
                            doc.Save(dfile);

                            XmlElement Director = doc.CreateElement("Director_Movie");
                            XmlElement dir_node = doc.CreateElement("Director");
                            dir_node.InnerText = Director_name;
                            Director.AppendChild(dir_node);
                            dir_node = doc.CreateElement("Movie_Name");
                            dir_node.InnerText = s + "," + Movie_name;
                            Director.AppendChild(dir_node);
                            doc.Load(dfile);
                            XmlElement dir_root = doc.DocumentElement;
                            dir_root.AppendChild(Director);
                            doc.Save(dfile);
                        }
                    }

                }


            }
        }
        private void load_directors_rate(string Director_name, string Movie_Rate)
        {
            String dfile = "";
            if (!File.Exists("Director&Rate.xml"))
            {
                XmlWriter writer = XmlWriter.Create("Director&Rate.xml");
                writer.WriteStartDocument();
                writer.WriteStartElement("Table");
                writer.WriteAttributeString("name", "DRate");

                writer.WriteStartElement("Director_Rate");
                writer.WriteStartElement("Director");
                writer.WriteString(Director_name);
                writer.WriteEndElement();

                writer.WriteStartElement("Movie_Rate");
                writer.WriteString(Movie_Rate);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();


            }
            else
            {

                dfile = "Director&Rate.xml";
                if (load_newMD(Director_name, dfile))
                {
                    XmlDocument dir_doc = new XmlDocument();
                    XmlElement Director = dir_doc.CreateElement("Director_Rate");
                    XmlElement dir_node = dir_doc.CreateElement("Director");
                    dir_node.InnerText = Director_name;
                    Director.AppendChild(dir_node);
                    dir_node = dir_doc.CreateElement("Movie_Rate");
                    dir_node.InnerText = Movie_Rate;
                    Director.AppendChild(dir_node);
                    dir_doc.Load(dfile);
                    XmlElement dir_root = dir_doc.DocumentElement;
                    dir_root.AppendChild(Director);
                    dir_doc.Save(dfile);
                    MessageBox.Show("new director with rate is added");

                }
                else
                {
                    //MessageBox.Show("hhhjbhjhjhkjhkuhk");
                    XmlDocument doc = new XmlDocument();
                    doc.Load(dfile);

                    foreach (XmlNode node in doc.SelectNodes("Table/Director_Rate"))
                    {
                        if (node.SelectSingleNode("Director").InnerText == Director_name)
                        {
                            MessageBox.Show("Director found ");
                            string s = node.SelectSingleNode("Movie_Rate").InnerText;
                            node.ParentNode.RemoveChild(node);
                            doc.Save(dfile);

                            XmlElement Director = doc.CreateElement("Director_Rate");
                            XmlElement dir_node = doc.CreateElement("Director");
                            dir_node.InnerText = Director_name;
                            Director.AppendChild(dir_node);
                            dir_node = doc.CreateElement("Movie_Rate");
                            dir_node.InnerText = s + "," + Movie_Rate;
                            Director.AppendChild(dir_node);
                            doc.Load(dfile);
                            XmlElement dir_root = doc.DocumentElement;
                            dir_root.AppendChild(Director);
                            doc.Save(dfile);
                        }
                    }

                }



            }

        }

        private bool load_newMD(string director_name, string dfile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dfile);
            XmlNodeList dlist = doc.GetElementsByTagName("Director");
            for (int i = 0; i < dlist.Count; i++)
            {
                String dvalue = dlist[i].InnerText;
                if (dvalue.Equals(director_name))
                {
                    return false;
                }
            }
            return true;
        }
        private bool load_newM(String text, string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNodeList tlist = doc.GetElementsByTagName("Title");
            for (int i = 0; i < tlist.Count; i++)
            {
                String tvalue = tlist[i].InnerText;
                if (tvalue.Equals(text))
                {
                    return false;
                }
            }
            return true;

        }
        private void Update_files()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Files.xml");
            //  XmlNodeList dlist = doc.GetElementsByTagName("name");

            //MessageBox.Show("upate files");
            foreach (XmlNode node in doc.SelectNodes("Files/File"))
            {
                String filename = node.SelectSingleNode("name").InnerText;
                //MessageBox.Show(filename);
                if (!File.Exists(filename + ".xml"))
                {
                    MessageBox.Show("file found");
                    // string s = node.SelectSingleNode("name").InnerText;
                    node.ParentNode.RemoveChild(node);
                    doc.Save("Files.xml");

                }
            }
        }
    }
}

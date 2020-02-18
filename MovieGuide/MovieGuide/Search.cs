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

namespace Movie_Guide
{

    public partial class Search : UserControl
    {
        public string nam = "";

        public Search()
        {
            InitializeComponent();
            movieClass m = new movieClass();
            m.viewAllMovieinSearch(flowLayoutPanel1);

        }


        private void Search_Load(object sender, EventArgs e)
        {


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            //movieClass mov = new movieClass();
            //mov.movieSearch(bunifuCustomLabel1.Text, comboBox4.Text, comboBox3.Text, comboBox2.Text, comboBox1.Text, radioButton2.Checked, checkedListBox1, this);


            string path = "";

            XmlDocument fileDoc = new XmlDocument();
            fileDoc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Files.xml");
            foreach (XmlNode node in fileDoc.SelectNodes("Files/File"))
            {
                if (comboBox4.Text == node.SelectSingleNode("name").InnerText)
                {
                    path = node.SelectSingleNode("path").InnerText;
                }
            }
            if (path == "")
            {
                path = "E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Allmovies.xml";
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);



            List<String> CheckedGenersFromCheckBox = new List<String>();
            foreach (string item in checkedListBox1.CheckedItems)
            {
                CheckedGenersFromCheckBox.Add(item);
            }



            for (int i = 10; i >= 1; i--)
                foreach (XmlNode node in xdoc.SelectNodes("Movies/Movie"))
                {

                    bool genderAccepted = false;

                    string filmGeners = node.SelectSingleNode("Genres").InnerText;
                    List<string> fileFilmGeners = new List<string>();
                    int start = 0;
                    for (int j = 0; j < filmGeners.Length; j++)
                    {
                        string s = ""; // the string that will contain the geners
                        int k = start;
                        while (true)
                        {
                            if (filmGeners[k] != ',')
                            {
                                s += filmGeners[k];
                                k++;
                            }
                            else
                            {
                                if (k == filmGeners.Length - 1)
                                {
                                    fileFilmGeners.Add(s);
                                    j = filmGeners.Length - 1;
                                    break;
                                }
                                else
                                {
                                    start = k + 1;
                                    fileFilmGeners.Add(s);
                                    j = start;
                                    break;
                                }
                            }
                        }
                    }

                    for (int p = 0; p < CheckedGenersFromCheckBox.Count; p++)
                    {
                        for (int q = 0; q < fileFilmGeners.Count; q++)
                        {
                            if (CheckedGenersFromCheckBox[p] == fileFilmGeners[q])
                            {
                                genderAccepted = true;
                                break;
                            }
                        }
                    }
                    
                    int fileRate = Convert.ToInt16(node.SelectSingleNode("Rate").InnerText);
                    int choosenRate = 0;
                    if (comboBox1.Text != "")
                        choosenRate = Convert.ToInt16(comboBox1.Text);




                    if (fileRate == i)
                    {
                        if ((bunifuMaterialTextbox1.Text == "" || bunifuMaterialTextbox1.Text == node.SelectSingleNode("Title").InnerText)
                        && (comboBox3.Text == "" || comboBox3.SelectedItem.ToString() == node.SelectSingleNode("Director").InnerText)
                        && (comboBox2.Text == "" || comboBox2.SelectedItem.ToString() == node.SelectSingleNode("Year").InnerText)
                        && (comboBox1.Text == "" || (choosenRate >= fileRate && radioButton1.Checked) || (choosenRate <= fileRate && radioButton2.Checked) || comboBox1.Text == fileRate.ToString())
                        && (checkedListBox1.CheckedItems.Count.ToString() == "0" || genderAccepted)
                      )

                        {
                            movie m = new movie();
                            m.movieName.Text = node.SelectSingleNode("Title").InnerText;
                            m.bunifuCustomLabel1.Text = node.SelectSingleNode("Rate").InnerText;
                            string picPath = node.SelectSingleNode("Poster").InnerText;
                            m.pictureBox1.Image = Image.FromFile(picPath);
                            flowLayoutPanel1.Controls.Add(m);
                        }

                    }
                }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}

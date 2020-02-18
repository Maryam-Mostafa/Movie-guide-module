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
    public partial class movie : UserControl
    {
        string name, rate, pic;

        public movie(string name, string rate, string pic)
        {

            this.name = name;
            this.rate = rate;
            this.pic = pic;
            InitializeComponent();
            create_movie_post();
        }
        private void create_movie_post()
        {
            movieName.Text = name;
            bunifuCustomLabel1.Text = rate;
            pictureBox1.Image = Image.FromFile(pic);
        }
        public movie()
        {
            InitializeComponent();
        }
        main m = new main();

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void movie_Load(object sender, EventArgs e)
        {

        } 
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            //if (!main.Instance.pnlContainer.Controls.ContainsKey("Update"))
            //{
                Update u = new Update();
                u.checkedListBox1.Visible = false;
                u.bunifuMaterialTextbox2.Text = movieName.Text;

                XmlDocument doc = new XmlDocument();
                doc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Allmovies.xml");
                foreach (XmlNode node in doc.SelectNodes("Movies/Movie"))
                {
                    if (u.bunifuMaterialTextbox2.Text == node.SelectSingleNode("Title").InnerText)
                    {
                        u.bunifuMaterialTextbox3.Text = node.SelectSingleNode("Id").InnerText;
                        u.bunifuMaterialTextbox1.Text = node.SelectSingleNode("Director").InnerText;
                        u.comboBox1.Text = node.SelectSingleNode("Year").InnerText;
                        u. comboBox2.Text = node.SelectSingleNode("Rate").InnerText;
                        string gener = node.SelectSingleNode("Genres").InnerText;
                        string newg = "";
                        for (int i = 0; i < gener.Length - 1; i++)
                        {
                               newg += gener[i];    
                        }
                            
                        u.bunifuCustomLabel8.Text = newg;
                        string picPath = node.SelectSingleNode("Poster").InnerText;
                        u.pictureBox1.Image = Image.FromFile(picPath);

                    }
                }

                u.Dock = DockStyle.Fill;
                main.Instance.pnlContainer.Controls.Add(u);
            //}
            main.Instance.pnlContainer.Controls["Update"].BringToFront();
           // main.Instance.btnBack.Visible = true;
            ///////////////////////////////////////////////////////
    



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            main mainn =new main();
            Search s =new Search();
            movieClass m = new movieClass();
            m.deleteMovie(movieName.Text, "E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Allmovies.xml");

            XmlDocument filedoc = new XmlDocument();
            filedoc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Files.xml");
            foreach (XmlNode node in filedoc.SelectNodes("Files/File"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(node.SelectSingleNode("path").InnerText);
                foreach (XmlNode filenode in file.SelectNodes("Movies/Movie"))
                {
                    if (movieName.Text == filenode.SelectSingleNode("Title").InnerText)
                    {
                        m.deleteMovie(movieName.Text, node.SelectSingleNode("path").InnerText);
                        this.Hide();
                        MessageBox.Show("movie deleted successfully");
                        

                    }
                }
            }
        }
        private void movieName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //if (!main.Instance.pnlContainer.Controls.ContainsKey("movieDetails"))
            //{
            
                movieDetails a = new movieDetails();
                a.Dock = DockStyle.Fill;
              // main.Instance.pnlContainer.Controls.Clear();
                main.Instance.pnlContainer.Controls.Add(a);

                main.Instance.pnlContainer.Controls["movieDetails"].BringToFront();
               // main.Instance.btnBack.Visible = true;

                a.bunifuCustomLabel1.Text = movieName.Text;
                XmlDocument doc = new XmlDocument();
                doc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Allmovies.xml");
                foreach (XmlNode node in doc.SelectNodes("Movies/Movie"))
                {
                    if (a.bunifuCustomLabel1.Text == node.SelectSingleNode("Title").InnerText)
                    {
                        a.bunifuCustomLabel3.Text = node.SelectSingleNode("Director").InnerText;
                        a.bunifuCustomLabel8.Text = node.SelectSingleNode("Year").InnerText;
                        a.bunifuCustomLabel9.Text = node.SelectSingleNode("Rate").InnerText;
                        string gener = node.SelectSingleNode("Genres").InnerText;
                        string newg = "";
                        for (int i = 0; i < gener.Length - 1; i++)
                            newg += gener[i];
                        a.bunifuCustomLabel10.Text = newg;
                        string picPath = node.SelectSingleNode("Poster").InnerText;
                        a.pictureBox1.Image = Image.FromFile(picPath);

                    }
                }
           // }
        }
    }
}

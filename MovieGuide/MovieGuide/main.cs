using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Movie_Guide
{
    public partial class main : Form
    {
        static main _obj;
        public static main Instance
        {
            get
            {
                if (_obj == null)
                {

                    _obj = new main();
                }
                return _obj;
            }
        }

        public Panel pnlContainer
        {
            get { return panel1; }
            set { panel1 = value; }
        }
        public Button btnBack
        {
            get { return button1; }
            set { button1 = value; }
        }

        movieClass movie = new movieClass();
        public main()
        {
            InitializeComponent();
            
        }

        private void main_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            _obj = this;
            MainMenu m = new MainMenu();
            m.Dock = DockStyle.Fill;
            movie.viewAllMovie(panel1);
            panel1.Controls.Add(m);
        }


        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Add A = new Add();
            panel1.Controls.Clear();
            panel1.Controls.Add(A);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
            Search s = new Search();
            panel1.Controls.Clear();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Director.xml");
            panel1.Controls.Add(s);
            foreach (XmlNode node in xdoc.SelectNodes("Table/Director"))
            {
                s.comboBox3.Items.Add(node.SelectSingleNode("Name").InnerText);
            }


            xdoc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Files.xml");
            panel1.Controls.Add(s);
            foreach (XmlNode node in xdoc.SelectNodes("Files/File"))
            {
                s.comboBox4.Items.Add(node.SelectSingleNode("name").InnerText);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            AddNewFile A = new AddNewFile();
            
            panel1.Controls.Clear();
            panel1.Controls.Add(A);
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_2(object sender, EventArgs e)
        {
            movie.viewAllMovie(panel1);

        }
        //bool viewd = false;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //i stoped this to check the load function
            //if (!viewd)
            //{
            //    viewd = true;
            //    movie.viewAllMovie(panel1);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* if (main.Instance.pnlContainer.Controls.ContainsKey("MainMenu"))
            {
                panel1.Controls["MainMenu"].BringToFront();
                movie.viewAllMovie(panel1);
                button1.Visible = false;
            }
            else if(main.Instance.pnlContainer.Controls.ContainsKey("Search"))
            {
                panel1.Controls["Search"].BringToFront();
                //Search s = new Search();
                //movie.viewAllMovieinSearch(s.flowLayoutPanel1);
                button1.Visible = false;
            }*/

        }
    }
    }


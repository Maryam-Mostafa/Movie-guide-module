using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Movie_Guide
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
            MainMenu MM = new MainMenu();
            main mm = new Movie_Guide.main();
            mm.panel1.Controls.Clear();
            mm.panel1.Controls.Add(MM);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("E:\\MovieGuide\\MovieGuide\\bin\\Debug\\Allmovies.xml");
            for (int i = 10; i >= 1; i--)
                foreach (XmlNode node in xdoc.SelectNodes("Movies/Movie"))
                {
                    int fileRate = Convert.ToInt16(node.SelectSingleNode("Rate").InnerText);
                    if (fileRate == i)
                    {
                        movie m = new movie();
                        m.movieName.Text = node.SelectSingleNode("Title").InnerText;
                        m.bunifuCustomLabel1.Text = node.SelectSingleNode("Rate").InnerText;
                        string picPath = node.SelectSingleNode("Poster").InnerText;
                        m.pictureBox1.Image = System.Drawing.Image.FromFile(picPath);
                        MM.flowLayoutPanel1.Controls.Add(m);

                    }
                }
        }
    }
}

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

    class movieClass
    {
        int id;
        string name;
        string director;
        string year;
        string gener;
        int rate;
        string poster;

        public void setId(int id)
        {
            this.id = id;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setDirector(string director)
        {
            this.director = director;
        }
        public void setYear(string year)
        {
            this.year = year;
        }
        public void setGener(string gener)
        {
            this.gener = gener;
        }
        public void setRate(int rate)
        {
            this.rate = rate;
        }
        public void setPoster(string poster)
        {
            this.poster = poster;
        }

        public int getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }
        public string getDirector()
        {
            return this.director;
        }
        public string getYear()
        {
            return this.year;
        }
        public string getGener()
        {
            return this.gener;
        }
        public int getRate()
        {
            return this.rate;
        }
        public string getPoster()
        {
            return this.poster;
        }
        public movieClass()
        {

        }
        public movieClass(int id, string name, string director, string year, string gener, int rate, string poster)
        {
            this.id = id;
            this.name = name;
            this.director = director;
            this.year = year;
            this.gener = gener;
            this.rate = rate;
            this.poster = poster;
        }

        public void addMovie(movieClass movie, string path)
        {

        }
        public void deleteMovie(string movieName, string path)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            foreach (XmlNode node in xdoc.SelectNodes("Movies/Movie"))
            {
                if (movieName == node.SelectSingleNode("Title").InnerText)
                {
                    node.ParentNode.RemoveChild(node);
                    xdoc.Save(path);
                    break;
                }
            }

        }

        public void updateMovie(movieClass movie, string path)
        {

        }

        public void viewAllMovie(Panel panel1)
        {
            MainMenu MM = new MainMenu();
            panel1.Controls.Clear();
            panel1.Controls.Add(MM);

            XmlDocument doc = new XmlDocument();
            doc.Load("Allmovies.xml");
            XmlNodeList list = doc.GetElementsByTagName("Movie");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList children = list[i].ChildNodes;
                movie m = new movie(children[1].InnerText, children[5].InnerText, children[6].InnerText);
                MM.flowLayoutPanel1.Controls.Add(m);
            }

        }

        public void viewAllMovieinSearch(FlowLayoutPanel panel1)
        {
            MainMenu MM = new MainMenu();
            panel1.Controls.Clear();
            panel1.Controls.Add(MM);

            XmlDocument doc = new XmlDocument();
            doc.Load("Allmovies.xml");
            XmlNodeList list = doc.GetElementsByTagName("Movie");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList children = list[i].ChildNodes;
                movie m = new movie(children[1].InnerText, children[5].InnerText, children[6].InnerText);
                MM.flowLayoutPanel1.Controls.Add(m);
            }

        }
    }

}
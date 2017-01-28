using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WindowsFormsBooks
{
    [Serializable]
    [XmlType("title")]
    public class Title 
    {
        //[Browsable(false)] //makes this propery not represented in DataGridView
        [XmlAttribute("lang")]
        public string Lang { get; set; }

        [XmlText]
        public string TitleName { get; set; }

        public Title(string title, string lan) 
        {
            TitleName = title;
            Lang = lan;
        }

        public Title() { }

    }

    [Serializable]
    [XmlType("book")]
    public class Book
    {
        //[DisplayName("Title")] //changes the name of the column in DataGrid view
        //[XmlElement(ElementName = "title")]

        [XmlIgnore]
        public string Title 
        { 
            get { return BookTitle.TitleName; }
            private set { BookTitle.TitleName = value; }
        }

        private string author;

        [XmlIgnore]
        public string Author 
        {
            get 
            {
                UpdateAuthor();
                return author;
            }
            set { author = value; }
        }

        [Browsable(false)] //makes this propery not represented in DataGridView
        [XmlElement("title")]
        public Title BookTitle { get; set; }

        [XmlAttribute("category")]
        public string Category { get; set; }
        
        [Browsable(false)] //makes this propery not represented in DataGridView
        [XmlElement("year")]
        public int Year { get; set; }

        //[Browsable(false)] //makes this propery not represented in DataGridView
        //[XmlIgnore]
        //public string Lang { get; set; }

        [Browsable(false)] //makes this propery not represented in DataGridView
        [XmlAttribute("cover")]
        public string Cover { get; set; }
        
        [XmlElement("price")]
        public double Price { get; set; }

        [Browsable(false)] //makes this propery not represented in DataGridView
        [XmlElement("author")]
        public List<string> Authors { get; set; }

        //for one author
        public Book(string title, string lang, string author, string category, int year, double price, string cover = null)
            :this()
        {
            this.Authors.Add(author);
            this.Author = author;

            InitFields(title, lang, category, year, price, cover);
        }
        //for list of authors
        public Book(string title, string lang, List<string> authors, string category, int year, double price, string cover = null)
        {
            this.Authors = authors;

            InitFields( title, lang, category, year, price, cover);
        }

        public Book() 
        {
            this.Authors = new List<string>();
        }

        private void InitFields(string title, string lang, string category, int year, double price, string cover = null) 
        {
            BookTitle = new Title(title, lang);
            //this.Lang = lang;
            this.Title = title;
            this.Category = category;
            this.Year = year;
            this.Price = price;
            this.Cover = cover;
        }

        private void UpdateAuthor() 
        {
            author = string.Empty;

            for (int i = 0; i < Authors.Count; i++ )
            {
                author += Authors[i];
                if ((i + 1) != Authors.Count)
                    author += "; ";
            }
        }
    }
}

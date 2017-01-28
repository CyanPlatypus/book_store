using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WindowsFormsBooks
{
    [Serializable]
    [XmlType("bookstore")]
    public class BookStore
    {
        [XmlElement("book")]
        public BindingList<Book> StoreBooksBindingList { get; set; }

        //[XmlIgnore]
        //public NewBookWindow BookWindow { get; set; }

        public BookStore() 
        {
            StoreBooksBindingList = new BindingList<Book>();

            //BookWindow = new NewBookWindow();
        }

        public void AddBook(string title, string lan, string author, string category, int year, double price, string cover = null)
        {
            StoreBooksBindingList.Add(new Book ( title, lan, author, category, year, price, cover));
        }

        public void AddBook(string title, string lang, List<string> authors, string category, int year, double price, string cover = null)
        {
            StoreBooksBindingList.Add(new Book(title, lang, authors, category, year, price, cover));
        }

        public void AddBook(Book book)
        {
            StoreBooksBindingList.Add(book);
        }

        public void RemoveBookAt(int index)
        {
            if (InRange(index))
                StoreBooksBindingList.RemoveAt(index);
        }

        private bool InRange(int index) 
        {
            return (index >= 0) && (index < StoreBooksBindingList.Count);
        }


    }
}

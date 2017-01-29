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

        public BookStore() 
        {
            StoreBooksBindingList = new BindingList<Book>();
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

        public Book ReturnBookAt(int index) 
        {
            if (InRange(index))
                return StoreBooksBindingList[index];
            return null;
        }

        private bool InRange(int index) 
        {
            return (index >= 0) && (index < StoreBooksBindingList.Count);
        }


    }
}

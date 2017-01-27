using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsBooks
{
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        //maybe enum?
        public string Category { get; private set; }
        public int Year { get; private set; }
        public double Price { get; private set; }

        public Book(string title, string author, string category, int year, double price)
        {
            this.Author = author;
            this.Title = title;
            this.Category = category;
            this.Year = year;
            this.Price = price;
        }
    }
}

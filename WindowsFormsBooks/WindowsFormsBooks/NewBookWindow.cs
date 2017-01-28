using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsBooks
{
    public partial class NewBookWindow : Form
    {
        public Book DisplayedBook { get; private set; }

        public NewBookWindow(Book book = null)
        {
            DisplayedBook = book;
            InitializeComponent();
            PlaceControls();
        }

        private void PlaceControls()
        {
            if (DisplayedBook != null)
            {
                titleTextBox.Text = DisplayedBook.Title;
                languageTextBox.Text = DisplayedBook.Language;
                categoryTextBox.Text = DisplayedBook.Category;
                yearTextBox.Text = Convert.ToString(DisplayedBook.Year);
                priceTextBox.Text = Convert.ToString(DisplayedBook.Price);
                coverTextBox.Text = DisplayedBook.Cover;
                authorTextBox.Text = DisplayedBook.Authors[0];

                for (int i = 1; i < DisplayedBook.Authors.Count; i++) 
                {
                    AddTextbox(DisplayedBook.Authors[i]);
                }

            }
        }

        private void AddTextbox(string text) 
        {
            TextBox tB = new TextBox()
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("MicrosoftSansSerif", 10),
                Height = 19,
                Width = 190,
                Text = text
            };

            tB.TextChanged += authorTB_TextChanged;

            tBoxflowLayoutPanel.Controls.Add(tB);
        }

        void authorTB_TextChanged(object sender, EventArgs e)
        {
            if (!Book.OkForAuthor(((TextBox)sender).Text))
                ((TextBox)sender).BackColor = Color.LightCoral;
            else
                ((TextBox)sender).BackColor = Color.White;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //no checking so far
            if (DisplayedBook != null)
            {

            }
            else
                DisplayedBook = new Book(titleTextBox.Text, "en", new List<string>{authorTextBox.Text},
                    categoryTextBox.Text, Convert.ToInt32(yearTextBox.Text), Convert.ToInt32(priceTextBox.Text)); 
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            AddTextbox(string.Empty);
        }
    }
}

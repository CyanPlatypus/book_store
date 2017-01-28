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
        //write null check
        public Book NewBook { get; private set; }

        public NewBookWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //no checking so far
            NewBook = new Book(titleTextBox.Text,"en", authorTextBox.Text,
                categoryTextBox.Text, Convert.ToInt32(yearTextBox.Text), Convert.ToInt32(priceTextBox.Text)); 
        }
    }
}

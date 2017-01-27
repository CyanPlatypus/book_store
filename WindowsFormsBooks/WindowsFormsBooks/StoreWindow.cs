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
    public partial class StoreWindow : Form
    {
        private BindingList<Book> storeBookBindingList;

        public StoreWindow()
        {
            storeBookBindingList = new BindingList<Book>(); 

            InitializeComponent();

            storeBookBindingList.Add(new Book("Emma comes back", "Tom Tompson", "History", 1999, 133));
            storeBookBindingList.Add(new Book("Tom comes back", "Tom Tompson", "History", 1998, 133));

            storeDataGridView.DataSource = storeBookBindingList;

            storeBookBindingList.Add(new Book("Olle comes back", "Tom Tompson", "History", 1998, 133));
        }

        private void PlaceAllColumns(DataGridView dgv) 
        {
            PlaceColumnWithTextAndName( "Title", "title", dgv);
            PlaceColumnWithTextAndName("Author", "author", dgv);
            PlaceColumnWithTextAndName("Category", "category", dgv);
            PlaceColumnWithTextAndName("Year", "year", dgv);
        }

        private void PlaceColumnWithTextAndName(string text, string name, DataGridView dgv) 
        {
            var column = new DataGridViewColumn();
            column.HeaderText = text;
            column.Name = name;
            column.CellTemplate = new DataGridViewTextBoxCell();

            dgv.Columns.Add(column);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in storeDataGridView.SelectedRows) 
            {
                if (row.Selected) 
                {
                    int index = row.Index;
                    //storeDataGridView.Rows.RemoveAt(index);
                    storeBookBindingList.RemoveAt(index);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            NewBookWindow nBWindiw = new NewBookWindow();

            if (nBWindiw.ShowDialog(this) == DialogResult.OK)
            {
                Book anotherBook = nBWindiw.NewBook;
                storeBookBindingList.Add(anotherBook);
            }

            nBWindiw.Dispose();
        }

    }
}

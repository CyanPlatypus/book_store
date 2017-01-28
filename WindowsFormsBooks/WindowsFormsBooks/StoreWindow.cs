using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsBooks
{
    public partial class StoreWindow : Form//, IStoreWindow
    {
        public BindingList<Book> DGVStoreSource { get; set; }

        public Book EditingBook { get; set; }

        private NewBookWindow AddBookDialogWindow;// { get; private set; }

        public event EventHandler<ObjectEventArgs> OpenButtonClicked;
        public event EventHandler<ObjectEventArgs> SaveButtonClicked;
        public event EventHandler<ObjectEventArgs> HtmlReportButtonClicked;

        public event EventHandler<ObjectEventArgs> DeleteButtonClicked;
        public event EventHandler<ObjectEventArgs> AddButtonClicked;
        public event EventHandler<ObjectEventArgs> EditButtonClicked;

        public StoreWindow(BindingList<Book> DGVSource)
        {
            //Dictionary<Type, string> dic = new Dictionary<Type, string> { { typeof(int), "hight" }, { typeof(string), "name" } };

            DGVStoreSource = DGVSource;

            //DGVStoreSource = new BookStore(); 

            InitializeComponent();

            //DGVStoreSource.AddBook("Emma comes back", "en", "Tom Tompson", "History", 1999, 133);
            //DGVStoreSource.AddBook("Tom comes back","en", "Tom Tompson", "History", 1998, 133);

            storeDataGridView.DataSource = DGVStoreSource;
            storeDataGridView.AllowUserToAddRows = false;

            //DGVStoreSource.AddBook("Anna comes back", "en", new List<string>{"Tilda Ma","Thory" }, "History", 1998, 133);
            //DGVStoreSource.AddBook("Danniel comes back", "en", "Tom Tompson", "History", 1998, 133, "Hard");

        }

        //private void PlaceAllColumns(DataGridView dgv) 
        //{
        //    PlaceColumnWithTextAndName( "Title", "title", dgv);
        //    PlaceColumnWithTextAndName("Author", "author", dgv);
        //    PlaceColumnWithTextAndName("Category", "category", dgv);
        //    PlaceColumnWithTextAndName("Year", "year", dgv);
        //}

        //private void PlaceColumnWithTextAndName(string text, string name, DataGridView dgv) 
        //{
        //    var column = new DataGridViewColumn();
        //    column.HeaderText = text;
        //    column.Name = name;
        //    column.CellTemplate = new DataGridViewTextBoxCell();

        //    dgv.Columns.Add(column);
        //}

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in storeDataGridView.SelectedRows) 
            {
                if (row.Selected) 
                {
                    int index = row.Index;
                    //storeDataGridView.Rows.RemoveAt(index);
                    if (DeleteButtonClicked != null)
                        DeleteButtonClicked(this, new ObjectEventArgs(index));
                    //DGVStoreSource.RemoveBookAt(index);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddBookDialogWindow = new NewBookWindow();

            if (AddBookDialogWindow.ShowDialog(this) == DialogResult.OK)
            {
                Book anotherBook = AddBookDialogWindow.DisplayedBook;
                if (AddButtonClicked != null)
                    AddButtonClicked(this, new ObjectEventArgs(anotherBook));
            }

            AddBookDialogWindow.Dispose();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files|*.xml";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) 
            {
                if (SaveButtonClicked != null)
                    SaveButtonClicked(this, new ObjectEventArgs(saveFileDialog.FileName));
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter= "XML files|*.xml|All files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {

                if (OpenButtonClicked != null)
                    OpenButtonClicked(this, new ObjectEventArgs(openFileDialog.FileName));

                storeDataGridView.DataSource = DGVStoreSource;
            }
            
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HTML files|*.html";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (HtmlReportButtonClicked != null)
                    HtmlReportButtonClicked(this, new ObjectEventArgs(saveFileDialog.FileName));
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in storeDataGridView.SelectedRows)
            {
                if (row.Selected)
                {
                    int index = row.Index;
                    if (EditButtonClicked != null)
                    {
                        EditButtonClicked(this, new ObjectEventArgs(index));
                        break;
                    }
                }
            }

            if (EditingBook != null)
            {
                AddBookDialogWindow = new NewBookWindow(EditingBook);

                AddBookDialogWindow.ShowDialog(this);
                //if (AddBookDialogWindow.ShowDialog(this) == DialogResult.OK)
                //{
                //    Book anotherBook = AddBookDialogWindow.NewBook;
                //    if (AddButtonClicked != null)
                //        AddButtonClicked(this, new ObjectEventArgs(anotherBook));
                //}

                AddBookDialogWindow.Dispose();
                EditingBook = null;
            }
        }

    }

    public class ObjectEventArgs : EventArgs
    {
        public object Data { get; private set; }

        public ObjectEventArgs(object data)
        {
            this.Data = data;
        }
    }
}

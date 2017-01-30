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
    public interface IBookStoreWindow 
    {
        BindingList<Book> DGVStoreSource { get; set; }
        Book EditingBook { get; set; }

        event EventHandler<ObjectEventArgs> OpenButtonClicked;
        event EventHandler<ObjectEventArgs> SaveButtonClicked;
        event EventHandler<ObjectEventArgs> HtmlReportButtonClicked;

        event EventHandler<ObjectEventArgs> DeleteButtonClicked;
        event EventHandler<ObjectEventArgs> AddButtonClicked;
        event EventHandler<ObjectEventArgs> EditButtonClicked;

        event EventHandler<ObjectEventArgs> ErrorOccurred;
    }

    public partial class StoreWindow : Form, IBookStoreWindow
    {
        public BindingList<Book> DGVStoreSource { get; set; } //souce for DataGridView

        public Book EditingBook { get; set; } // the Book that is being edited

        private NewBookWindow AddBookDialogWindow; //window for ading and aditing Book 

        public event EventHandler<ObjectEventArgs> OpenButtonClicked;
        public event EventHandler<ObjectEventArgs> SaveButtonClicked;
        public event EventHandler<ObjectEventArgs> HtmlReportButtonClicked;

        public event EventHandler<ObjectEventArgs> DeleteButtonClicked;
        public event EventHandler<ObjectEventArgs> AddButtonClicked;
        public event EventHandler<ObjectEventArgs> EditButtonClicked;

        public event EventHandler<ObjectEventArgs> ErrorOccurred;

        public StoreWindow(BindingList<Book> DGVSource)
        {
            DGVStoreSource = DGVSource;

            InitializeComponent();

            storeDataGridView.DataSource = DGVStoreSource;
            storeDataGridView.AllowUserToAddRows = false;

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //looks for all selected rows and gives their number to controller, using ObjectEventArgs
            foreach (DataGridViewRow row in storeDataGridView.SelectedRows) 
            {
                if (row.Selected) 
                {
                    int index = row.Index;

                    if (DeleteButtonClicked != null)
                        DeleteButtonClicked(this, new ObjectEventArgs(index));
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddBookDialogWindow = new NewBookWindow();
            AddBookDialogWindow.ErrorOccurred += AddBookDialogWindow_ErrorOccured;

            if (AddBookDialogWindow.ShowDialog(this) == DialogResult.OK)
            {
                //retrieves information about book from NewBookWindow
                Book anotherBook = AddBookDialogWindow.DisplayedBook;

                //generates event, that tells that the instance of the Book was made 
                //and sends that instans in ObjectEventArgs
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
                //generates event, that signalizes that user wants to open a file with name 
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
                AddBookDialogWindow.ErrorOccurred += AddBookDialogWindow_ErrorOccured;

                if (AddBookDialogWindow.ShowDialog(this) == DialogResult.OK)
                {
                    DGVStoreSource.ResetBindings();
                }

                AddBookDialogWindow.Dispose();
                
                EditingBook = null;
            }
        }

        private void AddBookDialogWindow_ErrorOccured(object sender, ObjectEventArgs e) 
        {
            if (ErrorOccurred != null)
                ErrorOccurred(this, e);
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

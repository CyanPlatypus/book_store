using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WindowsFormsBooks
{
    public class Controller
    {
        private BookStore controllerBookStore;
        public StoreWindow ControllerMainStoreWindow { get; private set; }
        public IMessager ControlerMessager { get; private set; }
        public XmlManager<BookStore> ControllerXMLManager { get; private set; }

        public Controller(IMessager messager) 
        {
            this.ControlerMessager = messager;
            this.ControllerXMLManager = new XmlManager<BookStore>();
            this.controllerBookStore = new BookStore();

            this.ControllerMainStoreWindow = new StoreWindow(controllerBookStore.StoreBooksBindingList);
            ControllerMainStoreWindow.OpenButtonClicked += ControllerMainStoreWindow_OpenButtonClicked;
            ControllerMainStoreWindow.SaveButtonClicked += ControllerMainStoreWindow_SaveButtonClicked;
            ControllerMainStoreWindow.HtmlReportButtonClicked += ControllerMainStoreWindow_HtmlReportButtonClicked;

            ControllerMainStoreWindow.AddButtonClicked += ControllerMainStoreWindow_AddButtonClicked;
            ControllerMainStoreWindow.DeleteButtonClicked += ControllerMainStoreWindow_DeleteButtonClicked;
            ControllerMainStoreWindow.EditButtonClicked += ControllerMainStoreWindow_EditButtonClicked;
            
        }

        void ControllerMainStoreWindow_EditButtonClicked(object sender, ObjectEventArgs e)
        {
            ControllerMainStoreWindow.EditingBook = controllerBookStore.ReturnBookAt((int)e.Data);
        }

        void ControllerMainStoreWindow_HtmlReportButtonClicked(object sender, ObjectEventArgs e)
        {
            string message = string.Empty;

            if (!ControllerXMLManager.TryConvertFromXMLAndResourcesToHTML((string)e.Data, "doc.xml", WindowsFormsBooks.Properties.Resources.bookstoreStyle, controllerBookStore, out message))
                ControlerMessager.ShowMessage(message);
        }

        void ControllerMainStoreWindow_DeleteButtonClicked(object sender, ObjectEventArgs e)
        {
            controllerBookStore.RemoveBookAt((int)e.Data);
        }

        void ControllerMainStoreWindow_AddButtonClicked(object sender, ObjectEventArgs e)
        {
            controllerBookStore.AddBook((Book)e.Data);
        }

        void ControllerMainStoreWindow_SaveButtonClicked(object sender, ObjectEventArgs e)
        {
            string message;

            string path = (string)e.Data;

            if (!ControllerXMLManager.TrySerializeToXML(path, controllerBookStore, out message))
                ControlerMessager.ShowMessage(message);
        }

        void ControllerMainStoreWindow_OpenButtonClicked(object sender, ObjectEventArgs e)
        {
            string message;

            string path = (string)e.Data;

            if(!ControllerXMLManager.TryDeserializeFromXML(path, ref controllerBookStore, out message))
                ControlerMessager.ShowError(message);
            else
                ControllerMainStoreWindow.DGVStoreSource = controllerBookStore.StoreBooksBindingList;
        }

    }
}

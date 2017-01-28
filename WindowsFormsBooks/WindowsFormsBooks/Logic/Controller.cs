using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsBooks
{
    public class Controller
    {
        //public BookStore ControllerStore { get { return controllerBookStore; } private set { controllerBookStore = value; } }
        private BookStore controllerBookStore;
        public StoreWindow ControllerMainStoreWindow { get; private set; }
        public IMessager ControlerMessager { get; private set; }
        public Serializer<BookStore> ControllerSerializer { get; private set; }

        public Controller(IMessager messager) 
        {
            this.ControlerMessager = messager;
            this.ControllerSerializer = new Serializer<BookStore>();
            this.controllerBookStore = new BookStore();

            this.ControllerMainStoreWindow = new StoreWindow(controllerBookStore.StoreBooksBindingList);
            ControllerMainStoreWindow.OpenButtonClicked += new EventHandler<ObjectEventArgs>(ControllerMainStoreWindow_OpenButtonClicked);
            ControllerMainStoreWindow.SaveButtonClicked += new EventHandler<ObjectEventArgs>(ControllerMainStoreWindow_SaveButtonClicked);
            ControllerMainStoreWindow.AddButtonClicked += new EventHandler<ObjectEventArgs>(ControllerMainStoreWindow_AddButtonClicked);
            ControllerMainStoreWindow.DeleteButtonClicked += new EventHandler<ObjectEventArgs>(ControllerMainStoreWindow_DeleteButtonClicked);
        }

        void ControllerMainStoreWindow_DeleteButtonClicked(object sender, ObjectEventArgs e)
        {
            controllerBookStore.RemoveBookAt((int)e.Data);
            //ControllerMainStoreWindow.DGVStoreSource = controllerBookStore.StoreBooksBindingList;
        }

        void ControllerMainStoreWindow_AddButtonClicked(object sender, ObjectEventArgs e)
        {
            controllerBookStore.AddBook((Book)e.Data);
            //ControllerMainStoreWindow.DGVStoreSource = controllerBookStore.StoreBooksBindingList;
        }

        void ControllerMainStoreWindow_SaveButtonClicked(object sender, ObjectEventArgs e)
        {
            string message;

            string path = (string)e.Data;

            if (!ControllerSerializer.Serialize(path, controllerBookStore, out message))
                ControlerMessager.ShowMessage(message);
            //ControllerMainStoreWindow.DGVStoreSource = controllerBookStore.StoreBooksBindingList;
        }

        void ControllerMainStoreWindow_OpenButtonClicked(object sender, ObjectEventArgs e)
        {
            string message;

            string path = (string)e.Data;

            if(!ControllerSerializer.Deserialize(path, ref controllerBookStore, out message))
                ControlerMessager.ShowError(message);
            else
                ControllerMainStoreWindow.DGVStoreSource = controllerBookStore.StoreBooksBindingList;
        }

    }
}

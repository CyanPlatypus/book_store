using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsBooks
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BookStore bs = new BookStore();
            Controller controller = new Controller(bs, new Messager(), new StoreWindow(bs.StoreBooksBindingList));

            Application.Run((Form)controller.ControllerMainStoreWindow);
        }
    }
}

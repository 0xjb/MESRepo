using System;
using System.Windows;

namespace MES.Starter
{
    class Starter : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            MainWindow mainWindow = new MainWindow();
            Application application = new Application();
            application.Run(mainWindow);

        }
    }
}

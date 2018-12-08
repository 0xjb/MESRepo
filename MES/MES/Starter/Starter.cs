using MES.Acquintance;
using MES.Data;
using MES.Logic;
using MES.Presentation;
using System;
using System.Windows;


namespace MES.Starter
{
    class Starter : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            IData data = new DataFacade();
            ILogic logic = new LogicFacade();
            IPresentation presentation = new PresentationFacade();
            logic.InjectData(data);
            presentation.InjectLogic(logic);
            Presentation.MainWindow mainWindow = new MainWindow(presentation);
            //LoginWindow loginWindow = new LoginWindow(presentation);

            Application application = new Application();
            //application.Run(loginWindow);
            //CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            application.Run(mainWindow);
        }
    }
}

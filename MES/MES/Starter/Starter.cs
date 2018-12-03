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

            MainWindow loginWindow = new MainWindow(presentation);

            Application application = new Application();
            application.Run(loginWindow);
        }
    }
}
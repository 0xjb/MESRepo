//using Acquaintance.IData;
//using Acquaintance.IPresentation;
//using Acquaintance.ILogic;
//using Data.DataFacade;
//using MES.Logic.LogicFacade;
//using PresentationFacade;
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


            MainWindow mainWindow = new MainWindow();
            Application application = new Application();
            application.Run(mainWindow);





        }



    }
}

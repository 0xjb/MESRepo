﻿using System;
using System.Windows;
//using Acquaintance.IData;
//using Acquaintance.IPresentation;
//using Acquaintance.ILogic;
//using Data.DataFacade;
//using MES.Logic.LogicFacade;
//using PresentationFacade;


namespace MES.Starter
{
    using Acquintance.IData;
    using Acquintance.ILogic;
    class Starter : Application
    {
        
        [STAThread]
        public static void Main(string[] args)
        {
            IData data = new DataFacade();
            ILogic logic = new LogicFacade();
            IPresentation presentation = new PresentationFacade();
            logic.
            
            MainWindow mainWindow = new MainWindow();
            Application application = new Application();
            application.Run(mainWindow);

        }
    }
}
